using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Coursework.Domain;

namespace Coursework.DAL;

public class FileTransactionRepository : ITransactionRepository
{
    private readonly string _filePath;

    public FileTransactionRepository(string filePath = "Data/transactions.json")
    {
        _filePath = filePath;
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void Save(IEnumerable<Transaction> transactions)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(transactions, options);
        File.WriteAllText(_filePath, json);
    }

    public IEnumerable<Transaction> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Transaction>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
    }
}