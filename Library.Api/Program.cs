using AutoMapper;
using Library.Api.Extensions;
using Library.Infrastructure.Context;
using Library.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Library.Application.Services;
using Library.Core.Abstractions.ServicesAbstractions;
using Library.Api.Middlewares;
using Library.Core.Abstractions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin() 
            .AllowAnyMethod() 
            .AllowAnyHeader()); 
});

builder.Services.AddSwagger();

builder.Services.AddValidation();

builder.Services.AddMappingProfiles();

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddUseCases();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddAuth(configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize(); 
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAllOrigins");


app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();
