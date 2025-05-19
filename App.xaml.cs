using DentalClinicApp.Data;
using System.Windows;

namespace DentalClinicApp
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var context = new ClinicContext())
            {
                context.Database.EnsureCreated(); // Создает базу данных, если её нет
            }
        }
    }
}