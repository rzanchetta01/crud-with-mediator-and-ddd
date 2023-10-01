using application;
using domain.Entities;
using domain.Mapping;
using FluentValidation;
using infrastructure;
using infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("db")));

builder.Services.AddSingleton<MapperlyMapper>();
builder.Services.AddScoped<IRepository<Team>, Repository<Team>>();
builder.Services.AddScoped<IRepository<Player>, Repository<Player>>();
builder.Services.AddScoped<IRepository<PlayerStat>, Repository<PlayerStat>>();
builder.Services.AddScoped<IRepository<TeamStat>, Repository<TeamStat>>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<ApplicationStartup>());
builder.Services.AddValidatorsFromAssemblyContaining<ApplicationStartup>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();