using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
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

// Add services to the container
builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

// Scoped services
builder.Services.AddScoped<ITaskRepositroy, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor();

// MediatR
builder.Services.AddMediatR(typeof(CreateUser).Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Swagger
builder.Services.AddOpenApi();

// Configuration
Configuration._configuration = builder.Configuration;
string? connectionstring = builder.Configuration.GetConnectionString("AppDbConnectionString");
Console.WriteLine(connectionstring);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

// Database
builder.Services.AddDbContext<appDbcontext>(options =>
{
    options.UseMySql(connectionstring, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        string? key = builder.Configuration["SecurityKey"];
        if (key == null) throw new ArgumentNullException(nameof(key));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Audience"]
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();