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
builder.Services.AddScoped<IRepository<User>, UserRepository>();

builder.Services.AddIdentity<User, IdentityRole>()
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

app.MapControllers();

app.Run();
