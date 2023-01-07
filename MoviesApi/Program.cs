using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Repository;
using MoviesApi.Repository.IRepository;
using MoviesApi.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("dbConnection");
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddResponseCaching();

builder.Services.AddDbContext<Context>(data => data.UseSqlServer(ConnectionString));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddAuthentication(data => {
    data.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    data.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(data => {
    data.RequireHttpsMetadata = false;
    data.SaveToken = true;
    data.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers(data => {
    data.CacheProfiles.Add("Default_20S", new CacheProfile() { Duration = 20 });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(data => {
    data.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "Authentication JWT With Bearer \r\n\r\n" +
                      "Insert 'Bearer' word following and next put the token below" +
                      "Example: Bearer TOKEN_HERE",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    data.AddSecurityRequirement(new OpenApiSecurityRequirement() {{
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer" },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header},
        new List<string>()
    }});
});

var app = builder.Build();

app.UseSwaggerUI(data => {
    data.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API V1");
});

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
