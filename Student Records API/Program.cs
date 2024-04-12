using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using Services.Repository;
using System;
using System.Text;

namespace Student_Records_API
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentRecordsApi", Version = "v1" });

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentRecordsApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
