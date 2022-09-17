using Application.API.Repository;
using Application.Repositories.Interface;
using Application.Repositories.Interfaces;
using Application.Repositories.Services;
using Application.Repository.Interfaces;
using Application.Repository.Services;
using Delgaarm.API.AutoMapper;
using Delgaarm.API.Extentions;
using Infrastracture.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Connection to database
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



//Identity Methods and jwt Bearer
builder.Services.AddIdentityServices(builder.Configuration);

// Service Scops
#region Service scopes
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<UserInterface, UserRepositoryService>();
builder.Services.AddScoped<IProductRepasitory, ProductService>();
#endregion
// Add services to the container.

// auto Mapper and Cloudinary configeration
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(policy => policy.AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:3001",
           "http://localhost:3002"));

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
