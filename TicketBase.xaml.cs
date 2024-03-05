using System.Collections.ObjectModel;
using System.Windows;

namespace Avia1
{
    public partial class TicketBase : Window
    {
        private ObservableCollection<Ticket> ticketCollection;
        private MainWindow mainWindow;

        public TicketBase(ObservableCollection<Ticket> tickets, MainWindow main)
        {
            InitializeComponent();
            ticketCollection = tickets;
            ticketBaseDataGrid.ItemsSource = ticketCollection;
            mainWindow = main;
        }

        // Метод для додавання квитка до колекції
        public void AddTicket(Ticket newTicket)
        {
            ticketCollection.Add(newTicket);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();


            mainWindow.Show();
        }
    }
}