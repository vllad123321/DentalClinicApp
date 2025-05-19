using DentalClinicApp.Data;
using System.Linq;
using System.Windows;

namespace DentalClinicApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new ClinicContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login);
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    var mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegistrationWindow();
            registerWindow.ShowDialog();
        }
    }
}