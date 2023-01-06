using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Repository;
using MoviesApi.Repository.IRepository;
using AutoMapper;
using MoviesApi.MoviesMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(data => data.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddAutoMapper(typeof(MoviesMapper));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
