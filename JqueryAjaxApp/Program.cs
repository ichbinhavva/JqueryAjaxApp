using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JqueryAjaxApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Models.OkulDbContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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
            // route : rota, yönlendirme, rotalama
            // pattern : desen
            // rota desenimize göre yazýyoruz. (localhost:5036/Ogrenci/Index )
            // controller yazmassak home olarak kabul edilecek, action'ý yazmassak index olarak kabul edilecek. Bu durumda hiçbir þey yazmassak ilk sayfa açýlýr.
            // id? : 

            app.Run();
        }
    }
}
