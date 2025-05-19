using DentalClinicApp.Data;
using System.Windows;

namespace DentalClinicApp
{
    public partial class UpdateStatusWindow : Window
    {
        private DentalClinicApp.Data.Application application;

        public UpdateStatusWindow(DentalClinicApp.Data.Application app)
        {
            InitializeComponent();
            application = app;
            StatusComboBox.SelectedIndex = (int)app.Status;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            application.Status = (ApplicationStatus)StatusComboBox.SelectedIndex;

            using (var context = new ClinicContext())
            {
                context.Applications.Update(application);
                context.SaveChanges();
            }

            DialogResult = true;
            Close();
        }
    }
}