using System;
using System.Collections.Generic;

namespace Coursework.Domain;

public interface IFinanceService
{
    void SetCategoryBudget(string name, decimal limit);
    void AddTransaction(Transaction transaction);
    void RemoveTransaction(Guid id);
    IEnumerable<Transaction> GetAll();
    IEnumerable<Transaction> Filter(TransactionFilterDelegate filter);
    decimal GetBalance();
    decimal GetPeriodSummary(DateTime start, DateTime end, TransactionType type);
}