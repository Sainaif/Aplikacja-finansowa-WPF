using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aplikacja_finansowa_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private FinanceManager financeManager = new FinanceManager();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
    {
        var amountInput = AmountTextBox.Text;
        if (!decimal.TryParse(amountInput, out var amount))
        {
            MessageBox.Show("Błędna kwota");
            return;
        }

        var description = DescriptionTextBox.Text;

        var transaction = new Transaction(amount, description);
        financeManager.AddTransaction(transaction);
    }

    private void ShowBalanceButton_Click(object sender, RoutedEventArgs e)
    {
        var balance = financeManager.GetBalance();
        BalanceLabel.Content = $"Twoje saldo wynosi: {balance}";
    }

    private void ShowTransactionHistoryButton_Click(object sender, RoutedEventArgs e)
    {
        var transactions = financeManager.GetTransactions();
        TransactionsListBox.Items.Clear();
        foreach (var transaction in transactions)
        {
            TransactionsListBox.Items.Add($"Kwota: {transaction.Amount}, Data: {transaction.Date}, Opis: {transaction.Description}");
        }
    }
}