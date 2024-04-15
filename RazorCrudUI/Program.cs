//this is where we put all of the services that we will end up using
// for this project, we will be using a database, so we need to add a database service
// later on, we will use dependency injection to inject this service into our controllers

using Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataMem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddDbContext<ItemsContext>(); --> this is calling the constructor of the ItemsContext class
builder.Services.AddDbContext<ItemsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
// builder.configuration refers to the appsettings.json file
// so anything in the appsettings.json file can be accessed using builder.configuration

builder.Services.AddScoped<IItemRepository, ItemRepositoryEf>();
//builder.Services.AddSingleton<IItemRepository, ItemRepositoryMem>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapGet("/", async context =>
//{
//    context.Response.Redirect("/items/");
//});
app.MapRazorPages();

app.Run();
