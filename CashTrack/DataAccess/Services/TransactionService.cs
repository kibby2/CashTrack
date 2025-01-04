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

            // Ensure there is enough balance for debit transactions
            if (transaction.transactionType == TransactionType.debit)
            {
                if (transaction.amount > currentBalance)
                {
                    return false; // Insufficient balance for debit
                }
            }

            // Handle credit transaction
            if (transaction.transactionType == TransactionType.credit)
            {
                transaction.remainingBalance = currentBalance + transaction.amount;
            }
            // Handle debit transaction
            else if (transaction.transactionType == TransactionType.debit)
            {
                transaction.remainingBalance = currentBalance - transaction.amount;
            }
            // Handle debt transaction
            else if (transaction.transactionType == TransactionType.debt)
            {
                transaction.remainingBalance = currentBalance; // Debt doesn't affect balance
                transaction.status = "unpaid";  // Explicitly set status to "unpaid" for debt transactions
            }

            _transactions.Add(transaction);  // Add the transaction to the list
            SaveTransactions(_transactions);  // Save the updated transactions list
            return true;  // Successfully added the transaction
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

        public async Task<bool> DeleteTransaction(Guid transactionId)
        {
            var transactionToDelete = _transactions.FirstOrDefault(t => t.transactionId == transactionId);

            if (transactionToDelete == null)
            {
                return false; // Transaction not found
            }

            _transactions.Remove(transactionToDelete);
            SaveTransactions(_transactions); // Save the updated list
            return true; // Successfully deleted the transaction
        }

        public async Task<bool> PayDebt(Guid transactionId)
        {
            var debtTransaction = _transactions.FirstOrDefault(t => t.transactionId == transactionId && t.transactionType == TransactionType.debt);

            if (debtTransaction == null)
            {
                return false; // Debt transaction not found
            }

            var currentBalance = await GetBalance();
            if (currentBalance < debtTransaction.amount)
            {
                return false; // Insufficient balance to pay the debt
            }

            debtTransaction.status = "Paid";  // Mark the debt as "Paid"
            SaveTransactions(_transactions);  // Save the updated transaction list
            return true; // Successfully updated the status to "Paid"
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
