using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Repository;
using MoviesApi.Repository.IRepository;
using MoviesApi.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(data => data.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddAutoMapper(typeof(Mapper));
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
