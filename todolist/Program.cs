using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using To_do_List.src.Modules.Tasks.Repository;
using To_do_List.src.Modules.User.Command;
using todolist.Helper.Auth.Service;
using todolist.src.Modules.User.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(config =>
{
    var policy =  new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
                                                   
});

//scope

builder.Services.AddScoped<ITaskRepositroy, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

//mediator

builder.Services.AddMediatR(typeof(CreateUser).Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//swagger

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "To-Do List API", Version = "v1" });

    // Add cookie-based authentication to Swagger
    options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Cookie,
        Name = CookieAuthenticationDefaults.AuthenticationScheme,
        Scheme = CookieAuthenticationDefaults.AuthenticationScheme,
        Description = "Enter your cookie for authentication."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "cookieAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});


var connectionstring = builder.Configuration.GetConnectionString("AppDbConnectionString");
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
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy",
        policy => policy.RequireClaim("Admin"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
