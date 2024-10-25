using DataLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Text;
using WineManagement.AppStarts;

var builder = WebApplication.CreateBuilder(args);

// Thêm OData
builder.Services.AddControllers()
    .AddOData(opt =>
    {
        opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100)
            .AddRouteComponents("odata", GetEdmModel());
    });


// Install DI and dbcontext
builder.Services.InstallService(builder.Configuration);

// Swagger config
builder.Services.ConfigureAuthService(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DependencyInjection
builder.Services.AddWebAPIService();

// DBcontext
builder.Services.AddDbContext<WineManagementSystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBDefault"));
});

var app = builder.Build();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    // Đăng ký các thực thể và mô hình OData ở đây
    builder.EntitySet<Account>("Accounts");
    builder.EntitySet<Role>("Roles");

    return builder.GetEdmModel();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
