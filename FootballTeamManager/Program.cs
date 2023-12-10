using FootballTeamManager.Data;
using FootballTeamManager.Mapper;
using FootballTeamManager.Repositorio;
using FootballTeamManager.Repositorio.IRepositorio;
using FootballTeamManager.Services;
using FootballTeamManager.Services.IServices;
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
builder.Services.AddScoped<IJugadorService, JugadorService>();

// Mapper

builder.Services.AddAutoMapper(typeof(EquipoMapper));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS

builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors("PolicyCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
