using System;

namespace DentalClinicApp.Data
{
    public enum ApplicationStatus
    {
        UnderConsideration, // На рассмотрении
        InProgress,         // В процессе
        Completed           // Завершено
    }

    public class Application
    {
        public int Id { get; set; }
        public string Article { get; set; }            // Заголовок
        public string ShortDescription { get; set; }   // Краткое описание
        public string Type { get; set; }               // Тип заявки
        public string FullDescription { get; set; }    // Полное описание
        public DateTime RegistrationDate { get; set; } // Дата регистрации
        public ApplicationStatus Status { get; set; }  // Статус
        public int? ExecutorId { get; set; }           // ID исполнителя
        public User Executor { get; set; }             // Исполнитель
        public DateOnly CreationDate { get; set; }     // Дата создания заявки
        public string RegistrationNumber { get; set; } // Номер заявки
    }
}