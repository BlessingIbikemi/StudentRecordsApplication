using Microsoft.EntityFrameworkCore;
using Services;
using Services.Repository;

namespace Student_Records_Web_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
            //     builder.Services.AddDbContext<DatabaseConnection>(o =>
            //o.UseSqlServer(connectionstring));

            builder.Services.AddDbContext<DatabaseConnection>(o =>
      o.UseInMemoryDatabase("StudentRecords"));

            // Add services to the container.
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Student}/{action=AddStudents}/{id?}");

            app.Run();
        }
    }
}
