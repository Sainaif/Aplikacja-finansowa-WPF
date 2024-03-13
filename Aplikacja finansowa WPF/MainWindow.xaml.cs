using System;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using System.ComponentModel;

namespace Aplikacja_finansowa_WPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        private string[] labels;
        public string[] Labels
        {
            get { return labels; }
            set
            {
                labels = value;
                OnPropertyChanged("Labels");
            }
        }

        private Func<double, string> formatter;
        public Func<double, string> Formatter
        {
            get { return formatter; }
            set
            {
                formatter = value;
                OnPropertyChanged("Formatter");
            }
        }

        private FinanceManager financeManager;

        public MainWindow()
        {
            financeManager = new FinanceManager(); // Initialize financeManager before InitializeComponent

            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth * 0.4; // 40% of screen width
            this.Height = SystemParameters.PrimaryScreenHeight * 0.4; // 40% of screen height

            UpdateChart(30); // Default to 30 days

            DataContext = this;
        }

        private void UpdateChart(int days)
        {
            var transactions = financeManager.GetTransactions().OrderBy(t => t.Date).ToList();
            if (days != -1) // If not "All"
            {
                var cutoffDate = DateTime.Now.AddDays(-days);
                transactions = transactions.Where(t => t.Date >= cutoffDate).ToList();
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Amount",
                    Values = new ChartValues<decimal>(transactions.Select(t => t.Amount))
                }
            };

            Labels = transactions.Select(t => t.Date.ToShortDateString()).ToArray();
            Formatter = value => value.ToString("C");
        }

        private void TimeFrameComboBox_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (TimeFrameComboBox.SelectedIndex)
            {
                case 0: // 7 days
                    UpdateChart(7);
                    break;
                case 1: // 30 days
                    UpdateChart(30);
                    break;
                case 2: // 90 days
                    UpdateChart(90);
                    break;
                case 3: // All
                    UpdateChart(-1);
                    break;
            }
        }

        private void OpenTransactionWindow_Click(object sender, RoutedEventArgs e)
        {
            // Pass the existing FinanceManager to the TransactionWindow
            TransactionWindow transactionWindow = new TransactionWindow(financeManager);
            if (transactionWindow.ShowDialog() == true)
            {
                // Update the chart after a transaction is added
                UpdateChart(TimeFrameComboBox.SelectedIndex == 3 ? -1 : int.Parse(((ComboBoxItem)TimeFrameComboBox.SelectedItem).Content.ToString().Split(' ')[0]));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}