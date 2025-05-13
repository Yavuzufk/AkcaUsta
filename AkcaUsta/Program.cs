using AkcaUsta.Context;
using AkcaUsta.Mapping;
using AkcaUsta.Repositories.IRepository;
using AkcaUsta.Repositories.Repository;
using AkcaUsta.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IAboutDal, AboutRepository>();
builder.Services.AddScoped<IBuisnessDataDAl, BuisnessDataRepository>();
builder.Services.AddScoped<IServiceDal, ServiceRepository>();
builder.Services.AddScoped<IServiceFeatureDal, ServiceFeatureRepository>();
builder.Services.AddScoped<IServiceRandevuDal, ServiceRandevuRepository>();
builder.Services.AddScoped<ITechnicianDal, TechnicianRepository>();
builder.Services.AddScoped<ITestimonialDal, TestimonialRepository>();
builder.Services.AddScoped<IContactDal, ContactRepository>();
builder.Services.AddScoped<IStatisticsDal, StatisticsRepository>();




var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=A_Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
