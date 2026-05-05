using System;

namespace Coursework.Domain;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string CategoryName { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }

    public Transaction() { }
    public Transaction(decimal amount, TransactionType type, string categoryName, string note)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Type = type;
        CategoryName = categoryName;
        Date = DateTime.Now;
        Note = note;
    }
}