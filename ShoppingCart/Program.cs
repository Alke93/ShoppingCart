using ShoppingCart.Generics;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Actions;
using ShoppingCart.DataAccessLayer.Db;
using ShoppingCart.DataAccessLayer.Implementation;
using ShoppingCart.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShoppingCart;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc().ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericControllerFeatureProvider()));
builder.Services.AddTransient<IService, Service>();
builder.Services.AddTransient<IGenericContext, GenericDbContext>();
builder.Services.AddTransient<IReadOnlyRepository, ReadOnlyRepository>();
builder.Services.AddTransient<IRepository, Repository>();



builder.Services.AddDbContext<GenericDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString(ConfigParams.DB)));
var app = builder.Build();
var serviceProvider = builder.Services.BuildServiceProvider();
var context = serviceProvider.GetRequiredService<GenericDbContext>();
try
{
    context.Database.Migrate();
}
catch (Exception ex)
{
    throw ex;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
