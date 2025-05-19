using Microsoft.EntityFrameworkCore;

namespace DentalClinicApp.Data
{
    public class ClinicContext : DbContext
    {
        public DbSet<Application> Applications { get; set; } // Таблица заявок
        public DbSet<User> Users { get; set; }               // Таблица пользователей

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=clinic.db"); // Подключение к SQLite
        }
    }
}