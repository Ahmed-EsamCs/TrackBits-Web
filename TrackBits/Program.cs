using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TrackBits.Data;
using TrackBits.Models.Entities;
using TrackBits.Services.SendGridEmailService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();







builder.Services.AddRazorPages();

//Email Service
builder.Services.Configure<SendGridSettings>(
    builder.Configuration.GetSection("SendGrid"));

builder.Services.AddTransient<ISendGridEmailService, SendGridEmailService>();
builder.Services.AddTransient<IEmailSender, IdentityEmailSender>();

builder.Services.AddScoped<ILicenseGeneratorService, LicenseGeneratorService>();




//Db Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();
app.MapRazorPages();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseRouting();

//Stripe Configuration for Payment Processing
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<String>();



app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
