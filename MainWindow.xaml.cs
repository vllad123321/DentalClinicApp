using DentalClinicApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace DentalClinicApp
{
    public partial class MainWindow : Window
    {
        private User currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            UserInfoLabel.Content = $"Пользователь: {user.FullName}, Зарегистрирован: {user.RegistrationDate}";
            LoadApplications();
        }

        private void LoadApplications()
        {
            using (var context = new ClinicContext())
            {
                ApplicationsGrid.ItemsSource = context.Applications.Include(a => a.Executor).ToList();
            }
        }

        private void AddApplication_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddApplicationWindow();
            if (addWindow.ShowDialog() == true)
            {
                LoadApplications();
            }
        }

        private void EditApplication_Click(object sender, RoutedEventArgs e)
        {
            var selectedApplication = ApplicationsGrid.SelectedItem as DentalClinicApp.Data.Application;
            if (selectedApplication != null)
            {
                var editWindow = new AddApplicationWindow(selectedApplication);
                if (editWindow.ShowDialog() == true)
                {
                    LoadApplications();
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.");
            }
        }

        private void DeleteApplication_Click(object sender, RoutedEventArgs e)
        {
            var selectedApplication = ApplicationsGrid.SelectedItem as DentalClinicApp.Data.Application;
            if (selectedApplication != null)
            {
                using (var context = new ClinicContext())
                {
                    context.Applications.Remove(selectedApplication);
                    context.SaveChanges();
                    LoadApplications();
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления.");
            }
        }

        private void AssignToMe_Click(object sender, RoutedEventArgs e)
        {
            var selectedApplication = ApplicationsGrid.SelectedItem as DentalClinicApp.Data.Application;
            if (selectedApplication != null)
            {
                using (var context = new ClinicContext())
                {
                    var app = context.Applications.Find(selectedApplication.Id);
                    app.ExecutorId = currentUser.Id;
                    context.SaveChanges();
                    LoadApplications();
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для назначения.");
            }
        }

        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            var selectedApplication = ApplicationsGrid.SelectedItem as DentalClinicApp.Data.Application;
            if (selectedApplication != null)
            {
                var statusWindow = new UpdateStatusWindow(selectedApplication);
                if (statusWindow.ShowDialog() == true)
                {
                    LoadApplications();
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для обновления статуса.");
            }
        }
    }
}