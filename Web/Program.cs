using DataAccess;
using Core.Entities;
using DataAccess.Contexts;
using Web.Services.Abstract;
using Web.Services.Concrete;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AdminAbstractServices = Web.Areas.Admin.Services.Abstract;
using AdminConcreteServices = Web.Areas.Admin.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IFileService, FileService>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false; 
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 0;
})
    .AddEntityFrameworkStores<AppDbContext>();

#region Repositories

builder.Services.AddScoped<IMedicalDepartmentRepository, MedicalDepartmentRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();
builder.Services.AddScoped<IPricingCardRepository, PricingCardRepository>();
builder.Services.AddScoped<IFAQCategoryRepository, FAQCategoryRepository>();
builder.Services.AddScoped<ILastestNewsRepository, LastestNewsRepository>();
builder.Services.AddScoped<IAboutPhotoRepository, AboutPhotoRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IWhyChooseRepository, WhyChooseRepository>();
builder.Services.AddScoped<IHomeVideoRepository, HomeVideoRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

#endregion

#region Services

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<AdminAbstractServices.IMedicalDepartmentService, AdminConcreteServices.MedicalDepartmentService>();
builder.Services.AddScoped<AdminAbstractServices.IProductCategoryService, AdminConcreteServices.ProductCategoryService>();
builder.Services.AddScoped<AdminAbstractServices.IHomeMainSliderService, AdminConcreteServices.HomeMainSliderService>();
builder.Services.AddScoped<AdminAbstractServices.ILastestNewsService, AdminConcreteServices.LastestNewsService>();
builder.Services.AddScoped<AdminAbstractServices.IPricingCardService, AdminConcreteServices.PricingCardService>();
builder.Services.AddScoped<AdminAbstractServices.IFAQCategoryService, AdminConcreteServices.FAQCategoryService>();
builder.Services.AddScoped<AdminAbstractServices.IStatisticsService, AdminConcreteServices.StatisticsService>();
builder.Services.AddScoped<AdminAbstractServices.IOurVisionService, AdminConcreteServices.OurVisionService>();
builder.Services.AddScoped<AdminAbstractServices.IWhyChooseService, AdminConcreteServices.WhyChooseService>();
builder.Services.AddScoped<AdminAbstractServices.IHomeVideoService, AdminConcreteServices.HomeVideoService>();
builder.Services.AddScoped<AdminAbstractServices.IQuestionService, AdminConcreteServices.QuestionService>();
builder.Services.AddScoped<AdminAbstractServices.IAccountService, AdminConcreteServices.AccountService>();
builder.Services.AddScoped<AdminAbstractServices.IAboutUsService, AdminConcreteServices.AboutUsService>();
builder.Services.AddScoped<AdminAbstractServices.IProductService, AdminConcreteServices.ProductService>();
builder.Services.AddScoped<AdminAbstractServices.IDoctorService, AdminConcreteServices.DoctorService>();

#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(userManager, roleManager);
}
app.Run();
