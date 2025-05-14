using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Filters;
using AkcaUsta.Mapping;
using AkcaUsta.Repositories.IRepository;
using AkcaUsta.Repositories.Repository;
using AkcaUsta.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Kimlik do�rulama (Authentication) servislerini ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Giri� sayfas�
        options.AccessDeniedPath = "/Home/AccessDenied"; // Eri�im reddedildi sayfas�
    });

// Authorization servisini ekleyin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// MVC ve View hizmetlerini ekleyin
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // E-posta do�rulamas� gerekmiyor
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IAboutDal, AboutRepository>();
builder.Services.AddScoped<IBuisnessDataDAl, BuisnessDataRepository>();
builder.Services.AddScoped<IServiceDal, ServiceRepository>();
builder.Services.AddScoped<IServiceFeatureDal, ServiceFeatureRepository>();
builder.Services.AddScoped<IServiceRandevuDal, ServiceRandevuRepository>();
builder.Services.AddScoped<ITechnicianDal, TechnicianRepository>();
builder.Services.AddScoped<ITestimonialDal, TestimonialRepository>();
builder.Services.AddScoped<IContactDal, ContactRepository>();
builder.Services.AddScoped<IStatisticsDal, StatisticsRepository>();
builder.Services.AddScoped<CustomAuthorizationFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Statik dosyalar i�in middleware
app.UseStaticFiles();

app.UseRouting();

// Authentication ve Authorization i�lemleri
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();