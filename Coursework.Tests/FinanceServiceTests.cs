using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Coursework.BLL;
using Coursework.Domain;

namespace Coursework.Tests;

public class FakeRepository : ITransactionRepository
{
    private List<Transaction> _data = new List<Transaction>();

    public void Save(IEnumerable<Transaction> transactions)
    {
        _data = transactions.ToList();
    }

    public IEnumerable<Transaction> Load()
    {
        return _data;
    }
}

public class FinanceServiceTests
{
    [Fact]
    public void AddTransaction_IncreasesBalance()
    {
        var repo = new FakeRepository();
        var service = new FinanceService(repo);
        var transaction = new Transaction(100, TransactionType.Income, "Salary", "");

        service.AddTransaction(transaction);

        Assert.Equal(100, service.GetBalance());
    }

    [Fact]
    public void AddTransaction_ExceedsBudget_ThrowsException()
    {
        var repo = new FakeRepository();
        var service = new FinanceService(repo);
        service.SetCategoryBudget("Food", 50);
        var transaction = new Transaction(100, TransactionType.Expense, "Food", "");

        Assert.Throws<BudgetExceededException>(() => service.AddTransaction(transaction));
    }
}