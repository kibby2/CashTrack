using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;
using System.Text.Json;

namespace CashTrack.DataAccess.Services
{
    public class TransactionService : ITransactionService
    {
        private List<Transaction> _transactions;

        public TransactionService()
        {
            _transactions = LoadTransactions();
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await Task.FromResult(_transactions);
        }

        public async Task<bool> AddTransaction(Transaction transaction)
        {
            var currentBalance = await GetBalance();

            if (transaction.transactionType == TransactionType.debit)
            {
                if (transaction.amount > currentBalance)
                {
                    return false; // Insufficient balance for debit
                }
            }

            if (transaction.transactionType == TransactionType.credit)
            {
                transaction.remainingBalance = currentBalance + transaction.amount;
            }
            else if (transaction.transactionType == TransactionType.debit)
            {
                transaction.remainingBalance = currentBalance - transaction.amount;
            }
            else if (transaction.transactionType == TransactionType.debt)
            {
                transaction.remainingBalance = currentBalance; // Debt doesn't affect balance
            }

            _transactions.Add(transaction);
            SaveTransactions(_transactions);
            return true;
        }

        public async Task<double> GetBalance()
        {
            var totalCredits = _transactions
                .Where(t => t.transactionType == TransactionType.credit)
                .Sum(t => t.amount);

            var totalDebits = _transactions
                .Where(t => t.transactionType == TransactionType.debit)
                .Sum(t => t.amount);

            return await Task.FromResult(totalCredits - totalDebits);
        }

        private List<Transaction> LoadTransactions()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.json");

            if (!File.Exists(filePath))
            {
                return new List<Transaction>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        private void SaveTransactions(List<Transaction> transactions)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.json");
            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(filePath, json);
        }
    }
}