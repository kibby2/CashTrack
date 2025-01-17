using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace CashTrack.Pages
{
    public partial class Credit_Debit : ComponentBase
    {
        private List<Transaction> allTransactions = new List<Transaction>();
        private List<Transaction> filteredTransactions = new List<Transaction>();
        private double balance = 0.0;
        private string searchQuery = string.Empty;
        private DateTime? startDate;
        private DateTime? endDate;

        [Inject] public ITransactionService transactionService { get; set; }

        // Load transactions on page initialization
        protected override async Task OnInitializedAsync()
        {
            allTransactions = await transactionService.GetAllTransactions();

            // Filter transactions to exclude debt transactions
            FilterTransactions();
            balance = await transactionService.GetBalance();
        }

        // Delete a transaction by its ID
        private async Task DeleteTransaction(Guid transactionId)
        {
            var success = await transactionService.DeleteTransaction(transactionId);
            if (success)
            {
                allTransactions = await transactionService.GetAllTransactions();
                FilterTransactions();
            }
            else
            {
                Console.WriteLine("Error: Transaction not found or could not be deleted.");
            }
        }

        // Filter transactions based on title, startDate, and endDate
        private void FilterTransactions()
        {
            filteredTransactions = allTransactions
                .Where(t => (t.transactionType == TransactionType.credit || t.transactionType == TransactionType.debit) && // Include only credit and debit
                            (string.IsNullOrEmpty(searchQuery) || t.title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) &&
                            (!startDate.HasValue || t.date >= startDate) &&
                            (!endDate.HasValue || t.date <= endDate))  // Apply AND logic between filters
                .OrderByDescending(t => t.date) // Sort by most recent
                .ToList();
        }

        
    }
}
