using System.Windows;

namespace Aplikacja_finansowa_WPF;

public partial class TransactionWindow : Window
{
    public Transaction Transaction { get; private set; }
    private FinanceManager financeManager;

    public TransactionWindow(FinanceManager financeManager)
    {
        InitializeComponent();
        this.financeManager = financeManager;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        decimal amount;
        if (!decimal.TryParse(AmountTextBox.Text, out amount))
        {
            MessageBox.Show("Invalid amount.");
            return;
        }

        string description = DescriptionTextBox.Text;
        Transaction = new Transaction(amount, description);

        // Add the transaction to the finance manager
        financeManager.AddTransaction(Transaction);

        this.DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
    }
}
