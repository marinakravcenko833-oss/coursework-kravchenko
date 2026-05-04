using System;
using System.Collections.Generic;

namespace Coursework.Domain;

public interface IFinanceService
{
    void AddTransaction(Transaction transaction);
    void RemoveTransaction(Guid id);
    IEnumerable<Transaction> GetAll();
    IEnumerable<Transaction> Filter(TransactionFilterDelegate filter);
    decimal GetBalance();
}