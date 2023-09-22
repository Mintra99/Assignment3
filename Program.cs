using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using Microsoft.OpenApi.Models;
using Assignment3.Services.Characters;
using Assignment3.Services.Franchises;
using Assignment3.Services.Movies;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<IFranchiseService, FranchiseService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddCors();



builder.Services.AddAuthorization();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmApi", Version = "v1" });
 
});





var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();


