using System;

namespace DentalClinicApp.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }             // Логин
        public string Password { get; set; }          // Пароль (хешированный)
        public DateTime RegistrationDate { get; set; } // Дата регистрации
        public string FullName { get; set; }          // Полное имя
        public string PhoneNumber { get; set; }       // Номер телефона
    }
}