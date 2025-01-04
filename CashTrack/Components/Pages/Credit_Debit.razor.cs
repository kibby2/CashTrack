using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashTrack.Pages
{
    // The component should inherit from ComponentBase
    public partial class Credit_Debit : ComponentBase
    {
        private List<Transaction> allTransactions = new List<Transaction>();
        private List<Transaction> filteredTransactions = new List<Transaction>();
        private double balance = 0.0;
        private string searchQuery = string.Empty;
        private DateTime? startDate;
        private DateTime? endDate;

        [Inject] public ITransactionService transactionService { get; set; }

        // The correct method signature for OnInitializedAsync
        protected override async Task OnInitializedAsync()
        {
            // Load all transactions from the service
            allTransactions = await transactionService.GetAllTransactions();
            // Initially set filtered transactions to all transactions
            filteredTransactions = allTransactions.Where(t => t.transactionType == TransactionType.credit || t.transactionType == TransactionType.debit).ToList();

            // Fetch the balance
            balance = await transactionService.GetBalance();
        }

        // Method to delete a transaction by its ID
        private async Task DeleteTransaction(Guid transactionId)
        {
            var success = await transactionService.DeleteTransaction(transactionId);
            if (success)
            {
                // Refresh transactions after deletion
                allTransactions = await transactionService.GetAllTransactions();
                FilterTransactions();
            }
            else
            {
                // Show an error or notification if deletion fails
                Console.WriteLine("Error: Transaction not found or could not be deleted.");
            }
        }

        // Method to apply filters to transactions
        private void FilterTransactions()
        {
            filteredTransactions = allTransactions
                .Where(t => (t.transactionType == TransactionType.credit || t.transactionType == TransactionType.debit) &&
                            (string.IsNullOrEmpty(searchQuery) || t.title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) &&
                            (!startDate.HasValue || t.date >= startDate) &&
                            (!endDate.HasValue || t.date <= endDate))
                .OrderByDescending(t => t.date) // Sort by most recent
                .ToList();
        }

        // Watch for changes to filters and re-apply them
        private void OnSearchQueryChanged()
        {
            FilterTransactions();
        }

        private void OnStartDateChanged()
        {
            FilterTransactions();
        }

        private void OnEndDateChanged()
        {
            FilterTransactions();
        }

        // Method to get the class for styling based on transaction type (credit = green, debit = red)
        private string GetTransactionClass(Transaction transaction)
        {
            return transaction.transactionType switch
            {
                TransactionType.credit => "table-success", // Green for Credit
                TransactionType.debit => "table-danger",   // Red for Debit
                _ => ""
            };
        }
    }
}
