using Microsoft.EntityFrameworkCore;
using TestTaskGFL.Models.Contexts;
using TestTaskGFL.Services;

var builder = WebApplication.CreateBuilder(args);

//Add db context
builder.Services.AddDbContext<FoldersContext>(options =>
{
    //Configure connection to the database with connection string from appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Add folder service
builder.Services.AddScoped<IFolderService, FolderService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.Run();
