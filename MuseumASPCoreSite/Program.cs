using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuseumSite.Application.Services;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain;
using MuseumSite.Domain.Entitites;
using MuseumSite.Domain.Repository;

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

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
