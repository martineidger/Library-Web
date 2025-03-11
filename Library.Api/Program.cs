using AutoMapper;
using Library.Api.Extensions;
using Library.Infrastructure.Context;
using Library.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Library.Application.Services;
using Library.Core.Abstractions.ServicesAbstractions;
using Library.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowAllOrigins",
    //    builder => builder
    //        .AllowAnyOrigin() // Разрешить все источники
    //        .AllowAnyMethod() // Разрешить все методы (GET, POST, и т.д.)
    //        .AllowAnyHeader()); // Разрешить все заголовки
    options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins("http://localhost:5173")
                              .AllowAnyMethod()
                              .AllowAnyHeader());
});

builder.Services.AddSwagger();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresLocal"));
});

builder.Services.AddValidation();

/////////
builder.Services.AddMappingProfiles();

builder.Services.AddRepositories();
builder.Services.AddControllers();
builder.Services.AddUseCases();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddAuth(configuration);


var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowSpecificOrigin");


app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();
