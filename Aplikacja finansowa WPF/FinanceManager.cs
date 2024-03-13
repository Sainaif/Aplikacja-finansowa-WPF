namespace Aplikacja_finansowa_WPF;

public class FinanceManager
{
    private List<Transaction> transactions = new List<Transaction>();

    public void AddTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
    }

    public decimal GetBalance()
    {
        return transactions.Sum(t => t.Amount);
    }

    public IEnumerable<Transaction> GetTransactions()
    {
        return transactions;
    }
}