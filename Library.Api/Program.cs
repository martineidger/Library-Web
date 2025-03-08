using AutoMapper;
using Library.Api.Extensions;
using Library.Infrastructure.Context;
using Library.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Library.Application.Services;
using Library.Core.Abstractions.ServicesAbstractions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSwagger();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresLocal"));
});

/////////
builder.Services.AddMappingProfiles();

builder.Services.AddRepositories();
builder.Services.AddControllers();
builder.Services.AddUseCases();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddAuth(configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();
