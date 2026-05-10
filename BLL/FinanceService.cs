using System;
using System.Collections.Generic;
using System.Linq;
using Coursework.Domain;

namespace Coursework.BLL;

public class FinanceService : IFinanceService
{
    private readonly ITransactionRepository _repository;
    private readonly List<Transaction> _transactions;

    public FinanceService(ITransactionRepository repository)
    {
        _repository = repository;
        _transactions = _repository.Load().ToList();
    }

    public void AddTransaction(Transaction transaction)
    {
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
}