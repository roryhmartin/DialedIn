using DialedUp.Application.ClockEntries;
using DialedUp.Application.Users;
using DialedUp.Domain.Roles;
using DialedUp.Domain.Users;
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

builder.Services.AddScoped<UsersApplicationService>();
builder.Services.AddScoped<ClockEntryApplicationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClockInAndOutRepository, ClockInAndOutRepository>();
builder.Services.AddScoped<UsersApplicationService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

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