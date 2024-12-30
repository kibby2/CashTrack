using CashTrack.DataAccess.Services.Interface;
using CashTrack.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace CashTrack.DataAccess.Services
{
    public class TransactionService : TransactionBase, ITransactionService
    {
        private List<Transaction> _transactions;
        private List<Tag> _tags;

        public TransactionService()
        {
            _transactions = LoadTransactions();  
            _tags = LoadTags();  
        }

        
        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await Task.FromResult(_transactions);  
        }

        
        public async Task<bool> AddTransaction(Transaction transaction)
        {
            var transactions = LoadTransactions();  

            
            if (transaction.tagID == null)
            {
               
                var newTag = new Tag("Default Tag");
                transaction.tagID = newTag;  
                _tags.Add(newTag);  
                SaveTags(_tags);  
            }

            
            if (transaction.transactionType == TransactionType.debit)
            {
                var totalCredits = transactions
                    .Where(t => t.transactionType == TransactionType.credit)
                    .Sum(t => t.amount);  

                
                if (transaction.amount > totalCredits)
                {
                    return false; 
                }

                
                transaction.remainingBalance = totalCredits - transaction.amount;
            }

            
            if (transaction.transactionType == TransactionType.credit)
            {
                
                transaction.remainingBalance = transactions
                    .Where(t => t.transactionType == TransactionType.credit)
                    .Sum(t => t.amount)
                    - transactions.Where(t => t.transactionType == TransactionType.debit).Sum(t => t.amount);
            }

            transactions.Add(transaction);  
            SaveTransactions(transactions);  
            return true;  
        }

       
        public async Task<bool> DeleteTransaction(Guid transactionId)
        {
            var transactions = LoadTransactions();  
            var transaction = transactions.FirstOrDefault(t => t.transactionId == transactionId);  

            if (transaction != null)
            {
                transactions.Remove(transaction);  
                SaveTransactions(transactions);  
                return true;  
            }

            return false;  
        }

       
        public async Task<double> GetTotalCredits()
        {
            var transactions = LoadTransactions();  
            return await Task.FromResult(
                transactions.Where(t => t.transactionType == TransactionType.credit).Sum(t => t.amount)  
            );
        }

        
        public async Task<List<Tag>> GetAllTags()
        {
            return await Task.FromResult(_tags);  
        }

        
        public async Task<bool> AddTag(Tag tag)
        {
            if (_tags.Any(t => t.TagID == tag.TagID))
            {
                return false;  
            }

            _tags.Add(tag);  
            SaveTags(_tags);  
            return true;  
        }

        
        private List<Transaction> LoadTransactions()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");

            if (!File.Exists(filePath))
            {
                return new List<Transaction>();  
            }

            var json = File.ReadAllText(filePath);  
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();  
        }

        
        private void SaveTransactions(List<Transaction> transactions)
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");

            var json = JsonSerializer.Serialize(transactions);  
            File.WriteAllText(filePath, json); 
        }

       
        private List<Tag> LoadTags()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "tags.json");

            if (!File.Exists(filePath))
            {
                return new List<Tag>();  
            }

            var json = File.ReadAllText(filePath);  
            return JsonSerializer.Deserialize<List<Tag>>(json) ?? new List<Tag>();  
        }

        
        private void SaveTags(List<Tag> tags)
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "tags.json");

            var json = JsonSerializer.Serialize(tags);  
            File.WriteAllText(filePath, json);  
        }
    }
}
