using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MuseumSite.Application.Services;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain;
using MuseumSite.Domain.Entitites;
using MuseumSite.Domain.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MuseumDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<IExhibitService, ExhibitService>();
builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
builder.Services.AddScoped<IMuseumNewsService, MuseumNewsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRepository<Exhibit>, ExhibitRepository>();
builder.Services.AddScoped<IExhibitionInterface, ExhibitionRepository>();
builder.Services.AddScoped<IRepository<MuseumNews>, MuseumNewsRepository>();
builder.Services.AddScoped<IRepository<UserEntity>, UserRepository>();

builder.Services.AddIdentity<UserEntity, IdentityRole>(opt =>
    {
        opt.User.RequireUniqueEmail = true;
        opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+123457890";
    }
)
    .AddEntityFrameworkStores<MuseumDbContext>()
.AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
builder.Services.AddAuthorizationBuilder();
builder.Services.AddAuthorization();

builder.Services.AddCors(options => options.AddPolicy("Allow_Any_API",
    policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Allow_Any_API");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
