using Anime.Application.Services;
using Anime.Data.Context;
using Anime.Data.Repositories;
using Anime.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Configurar autenticação JWT
var key = Encoding.ASCII.GetBytes("ChaveDeSegurançaAnime");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Adicionar serviços ao contêiner.
builder.Services.AddDbContext<AnimeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<AnimeService>();

builder.Services.AddControllers();
// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construir o aplicativo.
var app = builder.Build();

// Configurar o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Animes API V1");
});

app.MapControllers();

app.Run();