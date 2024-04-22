using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuseumSite.Domain;
using MuseumSite.Domain.Entitites;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MuseumDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<UserEntity>()
    .AddEntityFrameworkStores<MuseumDbContext>()
    .AddApiEndpoints();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<UserEntity>();

app.UseAuthorization();

app.MapControllers();

app.Run();
