using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace Avia1
{
    public partial class BookTicket : Window
    {
        private ObservableCollection<Ticket> ticketCollection;
        private MainWindow mainWindow;

        public BookTicket(ObservableCollection<Ticket> ticketCollection, MainWindow mainWindow)
        {
            InitializeComponent();
            this.ticketCollection = ticketCollection;
            this.mainWindow = mainWindow;

            // Налаштування ComboBox для місця призначення
            destinationComboBox.ItemsSource = new string[] { "Нью-Йорк", "Лондон", "Токіо", "Берлін", "Сан-Франциско" };

            // Налаштування ComboBox для типу подорожі
            tripTypeComboBox.ItemsSource = new string[] { "One Way", "Return" };

            // Налаштування ComboBox для класу
            classComboBox.ItemsSource = new string[] { "Економ клас", "Стандарт клас", "Бізнес клас" };
        }

        private void BookTicket_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка наявності введених даних
            if (string.IsNullOrWhiteSpace(destinationComboBox.Text) ||
                string.IsNullOrWhiteSpace(tripTypeComboBox.Text) ||
                string.IsNullOrWhiteSpace(classComboBox.Text) ||
                string.IsNullOrWhiteSpace(fullNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка правильності введення імені
            if (!IsValidName(fullNameTextBox.Text))
            {
                MessageBox.Show("Невірно вказане ім'я. Будь ласка, вказуйте тільки букви.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка правильності введення email
            if (!IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("Невірний формат Email. Будь ласка, вкажіть правильну Email адресу.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Створення нового квитка
            Ticket newTicket = new Ticket
            {
                FlightDestination = destinationComboBox.Text,
                Destination = destinationComboBox.Text,
                TripType = tripTypeComboBox.Text,
                Class = classComboBox.Text,
                FullName = fullNameTextBox.Text,
                Email = emailTextBox.Text
            };

            // Додавання квитка до колекції
            ticketCollection.Add(newTicket);

            // Очищення полів введення
            ClearInputFields();

            // Закриття вікна бронювання квитка
            this.Close();
        }

        private void ClearInputFields()
        {
            destinationComboBox.Text = string.Empty;
            tripTypeComboBox.Text = string.Empty;
            classComboBox.Text = string.Empty;
            fullNameTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
        }

        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закриття вікна бронювання квитка без збереження даних
            this.Close();
        }
    }
}
