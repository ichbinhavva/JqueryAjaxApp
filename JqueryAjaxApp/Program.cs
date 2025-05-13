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
            // route : rota, y�nlendirme, rotalama
            // pattern : desen
            // rota desenimize g�re yaz�yoruz. (localhost:5036/Ogrenci/Index )
            // controller yazmassak home olarak kabul edilecek, action'� yazmassak index olarak kabul edilecek. Bu durumda hi�bir �ey yazmassak ilk sayfa a��l�r.
            // id? : 

            app.Run();
        }
    }
}
