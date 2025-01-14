﻿using CashTrack.DataModel.Model;
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
            // Initially filter transactions to show only credit and debit
            filteredTransactions = allTransactions.Where(t => t.transactionType == TransactionType.credit || t.transactionType == TransactionType.debit).ToList();
            balance = await transactionService.GetBalance();
        }

        // Delete a transaction by its ID
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
                Console.WriteLine("Error: Transaction not found or could not be deleted.");
            }
        }

        // Filter transactions based on title, startDate, and endDate
        private void FilterTransactions()
        {
            filteredTransactions = allTransactions
                .Where(t => (t.transactionType == TransactionType.credit || t.transactionType == TransactionType.debit) &&
                            (string.IsNullOrEmpty(searchQuery) || t.title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) &&
                            (!startDate.HasValue || t.date >= startDate) &&
                            (!endDate.HasValue || t.date <= endDate))  // Apply AND logic between filters
                .OrderByDescending(t => t.date) // Sort by most recent
                .ToList();
        }

        // Helper method to determine the row color based on transaction type (credit = green, debit = red)
        private string GetTransactionClass(Transaction transaction)
        {
            return transaction.transactionType switch
            {
                TransactionType.credit => "table-success", // Green for Credit
                TransactionType.debit => "table-danger",   // Red for Debit
                _ => ""
            };
        }

        // Method triggered by the Search button click
        private void OnSearchClick()
        {
            FilterTransactions();  // Filter transactions based on applied filters
        }
    }
}
