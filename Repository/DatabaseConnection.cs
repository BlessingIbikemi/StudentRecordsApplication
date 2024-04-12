using Microsoft.EntityFrameworkCore;
using Services.Model;
using System;

namespace Services
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection(DbContextOptions<DatabaseConnection> options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }
    }
}
