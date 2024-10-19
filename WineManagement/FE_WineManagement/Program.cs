using BusinessLayer.Service.Interface;
using FE_WineManagement.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5067/") });

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AuthServices>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "Account",
       pattern: "login",
       defaults: new { controller = "Account", action = "Index" });

    endpoints.MapControllerRoute(
       name: "Register",
       pattern: "register",
       defaults: new { controller = "Register", action = "Index" });
    endpoints.MapControllerRoute(
      name: "Account",
      pattern: "account/signup",
      defaults: new { controller = "Account", action = "SignUp" });

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");

});
app.Run();
