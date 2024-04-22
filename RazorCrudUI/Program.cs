//this is where we put all of the services that we will end up using
// for this project, we will be using a database, so we need to add a database service
// later on, we will use dependency injection to inject this service into our controllers

using Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataMem;
using Microsoft.AspNetCore.Identity;
using UI.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddDbContext<ItemsContext>(); --> this is calling the constructor of the ItemsContext class
builder.Services.AddDbContext<ItemsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ItemsContext>();
// this adds the authorization policy to the application
// gives us access to a RoleManager, UserManager, SignInManager, IdentityUser, and IdentityRole

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(AdminHelper.ADMIN_ROLE));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Items", "AdminPolicy");
});

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

// this will run every time our website starts up
// we need to create a scope here to get access to our ServiceProvider
// which lets us get access to the RoleManager and UserManager
// using ensures everything gets disposed of properly when we are done
using (var scope = app.Services.CreateScope())
{
    await AdminHelper.SeedAdminAsync(scope.ServiceProvider);
}

app.Run();
