using DialedUp.Application.ClockEntries;
using DialedUp.Application.ClockEntries.Interfaces;
using DialedUp.Application.Users;
using DialedUp.Domain.Roles;
using DialedUp.Domain.Users;
using DialedUp.Facade.ClockEntries;
using DialedUp.Facade.ClockEntries.Interface;
using DialedUp.Facade.Users;
using DialedUp.Facade.Users.Interfaces;
using DialedUp.Persistance;
using DialedUp.Persistance.queries.roles;
using DialedUp.Persistance.queries.user;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//                        ?? "server=localhost;database=dialin;user=root;password=triumph9510";

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// === Persistence Layer (Repositories) ===
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClockInAndOutRepository, ClockInAndOutRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// === Application Layer (Business Logic) ===
builder.Services.AddScoped<IUserApplicationService, UsersApplicationService>();
builder.Services.AddScoped<IClockEntryApplicationService, ClockEntryApplicationService>();

// === Facade Layer (API DTOs & Mapping) ===
builder.Services.AddScoped<IUsersApplicationFacadeService, UsersApplicationFacadeService>();
builder.Services.AddScoped<IClockEntryFacadeApplicationService, ClockEntryFacadeApplicationService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();