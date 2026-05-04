using System.Collections.Generic;

namespace Coursework.Domain;

public interface ITransactionRepository
{
    void Save(IEnumerable<Transaction> transactions);
    IEnumerable<Transaction> Load();
}