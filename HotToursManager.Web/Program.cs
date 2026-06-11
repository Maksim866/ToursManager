using HotToursManager.Services;
using HotToursManager.Services.Contracts;
using HotToursManager.Storage.DataBase;
using HotToursManager.Storage.Contracts;
using Microsoft.EntityFrameworkCore;


namespace HotToursManager.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<TourDbContext>(options =>
                options.UseNpgsql(connection));
            builder.Services.AddScoped<ITourRepository, TourRepository>();
            builder.Services.AddScoped<IReader>(sp => sp.GetRequiredService<TourDbContext>());
            builder.Services.AddScoped<IWriter>(sp => sp.GetRequiredService<TourDbContext>());
            builder.Services.AddScoped<ITourService, TourService>();

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
                pattern: "{controller=Tours}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
