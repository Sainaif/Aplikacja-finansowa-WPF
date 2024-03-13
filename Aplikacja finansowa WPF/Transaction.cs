namespace Aplikacja_finansowa_WPF;

public class Transaction
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }

    public Transaction(decimal amount, string description)
    {
        Amount = amount;
        Date = DateTime.Now;
        Description = description ?? "Brak opisu";
    }
}