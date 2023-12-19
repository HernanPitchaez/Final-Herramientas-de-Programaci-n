using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proyecto_ClubDeportes.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClubContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ClubContext") ?? throw new InvalidOperationException("Connection string 'ClubContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ClubContext>();
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<IIncomeRecordService, IncomeRecordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
