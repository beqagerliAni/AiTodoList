using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using To_do_List.src.Modules.Tasks.Repository;
using To_do_List.src.Modules.User.Command;
using todolist.Helper.Auth.Service;
using todolist.Helper.Configuration;
using todolist.Helper.Jwt;
using todolist.src.Modules.User.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
    config.Filters.Add(new AuthorizeFilter(policy));

});

//scope

builder.Services.AddScoped<ITaskRepositroy, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor();
//mediator

builder.Services.AddMediatR(typeof(CreateUser).Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//swagger

builder.Services.AddOpenApi();


Configuration._configuration = builder.Configuration;
string? connectionstring = builder.Configuration.GetConnectionString("AppDbConnectionString");
Console.WriteLine(connectionstring);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));
// db

builder.Services.AddDbContext<appDbcontext>(options =>
{
    options.UseMySql(connectionstring, serverVersion)
     .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
});

// auth
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)

.AddJwtBearer(options =>
{
     
    string? key = builder.Configuration.GetConnectionString("SecurityKey");
    if(key == null) { throw new ArgumentNullException(nameof(key)); }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetConnectionString("Issuer"),
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetConnectionString("Audience")
    };
});

var app = builder.Build();

app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
