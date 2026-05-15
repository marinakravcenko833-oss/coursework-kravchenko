using System;
using System.Collections.Generic;
using System.Linq;
using Coursework.Domain;

namespace Coursework.BLL;

public class FinanceService : IFinanceService
{
    private readonly ITransactionRepository _repository;
    private readonly List<Transaction> _transactions;
    private readonly List<Category> _categories;

    public FinanceService(ITransactionRepository repository)
    {
        _repository = repository;
        _transactions = _repository.Load().ToList();
        _categories = new List<Category>();
    }

    public void SetCategoryBudget(string name, decimal limit)
    {
        _categories.RemoveAll(c => c.Name == name);
        _categories.Add(new Category { Name = name, BudgetLimit = limit });
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction.Type == TransactionType.Expense)
        {
            var category = _categories.FirstOrDefault(c => c.Name == transaction.CategoryName);
            if (category.BudgetLimit > 0)
            {
                decimal currentSpent = _transactions
                    .Where(t => t.CategoryName == transaction.CategoryName && t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                if (currentSpent + transaction.Amount > category.BudgetLimit)
                {
                    throw new BudgetExceededException($"Limit exceeded for {transaction.CategoryName}");
                }
            }
        }

        _transactions.Add(transaction);
        _repository.Save(_transactions);
    }

    public void RemoveTransaction(Guid id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction != null)
        {
            _transactions.Remove(transaction);
            _repository.Save(_transactions);
        }
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _transactions;
    }

    public IEnumerable<Transaction> Filter(TransactionFilterDelegate filter)
    {
        return _transactions.Where(t => filter(t));
    }

    public decimal GetBalance()
    {
        decimal income = _transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        decimal expense = _transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
        return income - expense;
    }

    public decimal GetPeriodSummary(DateTime start, DateTime end, TransactionType type)
    {
        return _transactions
            .Where(t => t.Type == type && t.Date >= start && t.Date <= end)
            .Sum(t => t.Amount);
    }
}