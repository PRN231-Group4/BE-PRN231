using DataLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using View_Wine.AppStart;
using View_Wine.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Thêm dịch vụ cache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của session
    options.Cookie.HttpOnly = true; // Chỉ cho phép cookie được truy cập qua HTTP
    options.Cookie.IsEssential = true; // Cookie cần thiết cho hoạt động của ứng dụng
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5067/") });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCloudinary();
builder.Services.AddHttpClient();

builder.Services.AddScoped<AccountService>();
//DBcontext
builder.Services.AddDbContext<WineManagementSystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBDefault"));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.LoginPath = "/";
           options.LogoutPath = "/Home/Logout"; // Đường dẫn đến trang đăng xuất
       });
builder.Services.AddAutoMapper(typeof(AutoMap).Assembly);

// Cấu hình dịch vụ xác thực JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Iamnotsurewhattoputinthissection")) 
    };
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Cấu hình phân quyền
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerOrStaff", policy => policy.RequireRole("Manager", "Staff"));
});

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Login}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Wines",
      pattern: "wines",
      defaults: new { controller = "Wines", action = "Index" });

    endpoints.MapControllerRoute(
      name: "Login",
      pattern: "login",
      defaults: new { controller = "Login", action = "Index" });

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();
