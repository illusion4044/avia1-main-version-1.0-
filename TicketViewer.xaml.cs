using System.Collections.ObjectModel;
using System.Windows;

namespace Avia1
{
    public partial class TicketViewer : Window
    {
        private ObservableCollection<Ticket> ticketCollection;

        public TicketViewer(ObservableCollection<Ticket> tickets)
        {
            InitializeComponent();
            ticketCollection = tickets;
            ticketViewerDataGrid.ItemsSource = ticketCollection;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}