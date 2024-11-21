using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Api.Configurations;
using MultiShop.Catalog.Api.Extensions;
using MultiShop.Catalog.Api.Services.AboutServices;
using MultiShop.Catalog.Api.Services.BrandServices;
using MultiShop.Catalog.Api.Services.CategoryServices;
using MultiShop.Catalog.Api.Services.ContactServices;
using MultiShop.Catalog.Api.Services.FeatureServices;
using MultiShop.Catalog.Api.Services.FeatureSliderServices;
using MultiShop.Catalog.Api.Services.OfferDiscountServices;
using MultiShop.Catalog.Api.Services.ProductDetailServices;
using MultiShop.Catalog.Api.Services.ProductImageServices;
using MultiShop.Catalog.Api.Services.ProductServices;
using MultiShop.Catalog.Api.Services.SpecialOfferServices;
using MultiShop.Catalog.Api.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

TimeZoneInfo azTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
DateTime currentTimeInAZT = TimeZoneInfo.ConvertTime(DateTime.UtcNow, azTimeZone);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCatalog";
    opt.RequireHttpsMetadata = false;
});

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
