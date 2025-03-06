using Library.Application.UseCases.Authors;
using Library.Application.UseCases.Books;
using Library.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresLocal"));
});

builder.Services.AddControllers();

builder.Services.Scan(scan => scan
        .FromAssemblyOf<AddAuthorUseCase>() 
        .AddClasses(classes => classes.Where(type => type.Name.EndsWith("UseCase")))
        .AsSelf() 
        .WithScopedLifetime());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
