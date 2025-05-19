using DentalClinicApp.Data;
using System.Windows;

namespace DentalClinicApp
{
    public partial class AddApplicationWindow : Window
    {
        private DentalClinicApp.Data.Application application;

        public AddApplicationWindow()
        {
            InitializeComponent();
            application = new DentalClinicApp.Data.Application { RegistrationDate = DateTime.Now, Status = ApplicationStatus.UnderConsideration };
        }

        public AddApplicationWindow(DentalClinicApp.Data.Application existingApplication)
        {
            InitializeComponent();
            application = existingApplication;
            ArticleTextBox.Text = application.Article;
            ShortDescriptionTextBox.Text = application.ShortDescription;
            TypeTextBox.Text = application.Type;
            FullDescriptionTextBox.Text = application.FullDescription;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            application.Article = ArticleTextBox.Text;
            application.ShortDescription = ShortDescriptionTextBox.Text;
            application.Type = TypeTextBox.Text;
            application.FullDescription = FullDescriptionTextBox.Text;

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
    }
}