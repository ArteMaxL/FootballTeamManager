using FootballTeamManager.Data;
using FootballTeamManager.Mapper;
using FootballTeamManager.Repositorio;
using FootballTeamManager.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// BD Config

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ConexionSql"));
});

// Repos

builder.Services.AddScoped<IEquipoRepositorio, EquipoRepositorio>();
builder.Services.AddScoped<IJugadorRepositorio, JugadorRepositorio>();

// Mapper

builder.Services.AddAutoMapper(typeof(EquipoMapper));

// Add services to the container.

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
