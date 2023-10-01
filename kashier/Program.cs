using kashier.DAL.Database;
using Microsoft.EntityFrameworkCore;
using kashier.BL.Interface;
using kashier.BL.Mapper;
using kashier.BL.Repository;
using kashier.DAL.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITicketRep, RepTickets>();
builder.Services.AddDbContextPool<DBContainer>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("TicketsConnection")));
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

builder.Services.AddMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
builder.Services.AddMvcCore();
builder.Services.AddSingleton<ITempDataProvider,CookieTempDataProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tickets}/{action=Index}/{id?}");

app.Run();
