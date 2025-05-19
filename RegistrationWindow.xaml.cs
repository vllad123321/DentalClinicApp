using DentalClinicApp.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DentalClinicApp
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string fullName = FullNameTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Все поля обязательны для заполнения.");
                return;
            }

            using (var context = new ClinicContext())
            {
                if (context.Users.Any(u => u.Login == login))
                {
                    MessageBox.Show("Пользователь уже существует.");
                    return;
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var user = new User
                {
                    Login = login,
                    Password = hashedPassword,
                    RegistrationDate = DateTime.Now,
                    FullName = fullName,
                    PhoneNumber = phoneNumber
                };

                context.Users.Add(user);
                context.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно.");
                this.Close();
            }
        }
    }
}