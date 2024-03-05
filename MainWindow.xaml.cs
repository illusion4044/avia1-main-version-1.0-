using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace Avia1
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Ticket> ticketCollection = new ObservableCollection<Ticket>();
        private TicketBase ticketBaseWindow;

        public MainWindow()
        {
            InitializeComponent();
            ticketBaseWindow = new TicketBase(ticketCollection, this);
        }

        private void BookTicket_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(destinationComboBox.Text) ||
                string.IsNullOrWhiteSpace(tripTypeComboBox.Text) ||
                string.IsNullOrWhiteSpace(classComboBox.Text) ||
                string.IsNullOrWhiteSpace(fullNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidName(fullNameTextBox.Text))
            {
                MessageBox.Show("Невірно вказане ім'я. Будь ласка, вказуйте тільки букви.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("Невірний Email формат. Будь ласка, вкажіть правильний Email адресу.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get destination, trip type, and class from ComboBox
            string destination = destinationComboBox.Text;
            string tripType = tripTypeComboBox.Text;
            string ticketClass = classComboBox.Text;

            // Check if the flight is available in the current flights list
            bool isFlightAvailable = false;
            foreach (var flight in CurrentFlightsWindow.CurrentFlightsList)
            {
                if (flight.Destination == destination && flight.TripType == tripType && flight.Class == ticketClass)
                {
                    isFlightAvailable = true;
                    break;
                }
            }

            if (!isFlightAvailable)
            {
                MessageBox.Show($"Немає поточних квитків до {destination} в {ticketClass} для {tripType} типу подорожі.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new ticket
            Ticket newTicket = new Ticket
            {
                FlightDestination = destination,
                Destination = destination,
                TripType = tripType,
                Class = ticketClass,
                FullName = fullNameTextBox.Text,
                Email = emailTextBox.Text
            };

            // Add the ticket to the collection
            ticketCollection.Add(newTicket);

            
            ClearInputFields();

            // Hide the current MainWindow and show the TicketBase window
            this.Hide();
            ticketBaseWindow.Show();
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

        private void ViewTickets_Click(object sender, RoutedEventArgs e)
        {
            TicketViewer ticketViewerWindow = new TicketViewer(ticketCollection);
            ticketViewerWindow.Show();
        }

        private void CurrentFlightsButton_Click_1(object sender, RoutedEventArgs e)
        {
            CurrentFlightsWindow currentFlightsWindow = new CurrentFlightsWindow();
            currentFlightsWindow.Show();
        }

        private void BookTicketButton_Click(object sender, RoutedEventArgs e)
        {
            BookTicket bookTicketWindow = new BookTicket(ticketCollection, this);
            bookTicketWindow.Show();
        }

        private void LoyalityButton_Click(object sender, RoutedEventArgs e)
        {
            Loyality loyality = new Loyality();
            loyality.Show();
        }

        // Close all other windows when the main window is closed
        ~MainWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
        }
    }
}
