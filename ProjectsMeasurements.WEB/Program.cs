using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Repositories;
using ProjectsMeasurements.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add<ProjectsMeasurements.WEB.ActionFilters.ViewBagActionFilter>();
});
builder.Services.AddDbContext<ProjectsMeasurementsDBContext>(O=>O.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddScoped(serviceType: typeof(IUnitOfWork), implementationType: typeof(UnitOfWork));
builder.Services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));
builder.Services.AddScoped(serviceType: typeof(IRecursiveRepository<>), implementationType: typeof(RecursiveRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Services.CreateAsyncScope().ServiceProvider.GetService<ProjectsMeasurementsDBContext>()?.Database.Migrate();
app.Run();
