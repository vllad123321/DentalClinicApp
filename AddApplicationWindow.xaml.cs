using DentalClinicApp.Data;
using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicApp
{
    public partial class AddApplicationWindow : Window
    {
        private DentalClinicApp.Data.Application application;

        public AddApplicationWindow()
        {
            InitializeComponent();
            application = new DentalClinicApp.Data.Application
            {
                RegistrationDate = DateTime.Now,
                Status = ApplicationStatus.UnderConsideration,
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            };
            CreationDatePicker.SelectedDate = DateTime.Today;
        }

        public AddApplicationWindow(DentalClinicApp.Data.Application existingApplication)
        {
            InitializeComponent();
            application = existingApplication;
            ArticleTextBox.Text = application.Article;
            ShortDescriptionTextBox.Text = application.ShortDescription;
            TypeTextBox.Text = application.Type;
            FullDescriptionTextBox.Text = application.FullDescription;
            CreationDatePicker.SelectedDate = application.CreationDate.ToDateTime(TimeOnly.MinValue);
            RegistrationNumberTextBox.Text = application.RegistrationNumber;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ArticleTextBox.Text) ||
                string.IsNullOrEmpty(ShortDescriptionTextBox.Text) ||
                string.IsNullOrEmpty(TypeTextBox.Text) ||
                string.IsNullOrEmpty(FullDescriptionTextBox.Text) ||
                string.IsNullOrEmpty(RegistrationNumberTextBox.Text) ||
                !CreationDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Все поля обязательны для заполнения.");
                return;
            }

            application.Article = ArticleTextBox.Text;
            application.ShortDescription = ShortDescriptionTextBox.Text;
            application.Type = TypeTextBox.Text;
            application.FullDescription = FullDescriptionTextBox.Text;
            application.CreationDate = DateOnly.FromDateTime(CreationDatePicker.SelectedDate.Value);
            application.RegistrationNumber = RegistrationNumberTextBox.Text;

            try
            {
                using (var context = new ClinicContext())
                {
                    if (application.Id == 0)
                    {
                        context.Applications.Add(application);
                    }
                    else
                    {
                        context.Applications.Update(application);
                    }
                    context.SaveChanges();
                }
                DialogResult = true;
                Close();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }
        }
    }
}