using System;
using System.Text.Json;

namespace CashTrack.DataModel.Model
{
    public abstract class TransactionBase
    {
        

        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");

        
        protected List<Transaction> LoadTransactions()
        {
            if (!File.Exists(FilePath)) return new List<Transaction>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

       
        protected void SaveTransactions(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(FilePath, json);
        }
    }


}
