using System;
using Coursework.BLL;
using Coursework.Domain;

namespace Coursework.UI;

public class ConsoleMenu
{
    private readonly IFinanceService _financeService;

    public ConsoleMenu(IFinanceService financeService)
    {
        _financeService = financeService;
    }

    public void Run()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("--- Finance Manager ---");
            Console.WriteLine($"Current Balance: {_financeService.GetBalance()}");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. View All Transactions");
            Console.WriteLine("0. Exit");
            Console.Write("Select option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTransactionUI(TransactionType.Income);
                    break;
                case "2":
                    AddTransactionUI(TransactionType.Expense);
                    break;
                case "3":
                    ViewTransactions();
                    break;
                case "0":
                    isRunning = false;
                    break;
            }
        }
    }

    private void AddTransactionUI(TransactionType type)
    {
        Console.Clear();
        Console.WriteLine($"--- Add {type} ---");
        
        Console.Write("Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        
        Console.Write("Category: ");
        string category = Console.ReadLine();
        
        Console.Write("Note: ");
        string note = Console.ReadLine();

        try
        {
            var transaction = new Transaction(amount, type, category, note);
            _financeService.AddTransaction(transaction);
            Console.WriteLine("Success! Press any key...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Press any key to continue...");
        }
        Console.ReadKey();
    }

    private void ViewTransactions()
    {
        Console.Clear();
        Console.WriteLine("--- Transactions ---");
        var transactions = _financeService.GetAll();
        
        foreach (var t in transactions)
        {
            Console.WriteLine($"[{t.Date:dd.MM.yyyy}] {t.Type} - {t.Amount} ({t.CategoryName}) | {t.Note}");
        }
        
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }
}