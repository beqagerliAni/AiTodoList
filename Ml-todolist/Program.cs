using Ml_todolist.src.ML;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MLModel>();
// 2. Configure JSON serialization for Unicode
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

Console.OutputEncoding = Encoding.UTF8;

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();