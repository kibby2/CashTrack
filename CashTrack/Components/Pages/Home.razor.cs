using Microsoft.AspNetCore.Components;
using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;

namespace CashTrack.Pages
{
    public partial class Home : ComponentBase
    {
        private List<Transaction> creditTransactions = new List<Transaction>();
        private List<Transaction> debitTransactions = new List<Transaction>();
        private List<Transaction> debtTransactions = new List<Transaction>();

        [Inject] public ITransactionService TransactionService { get; set; }

        // Ensure this method signature is correct
        protected override async Task OnInitializedAsync()
        {
            var allTransactions = await TransactionService.GetAllTransactions();
            var oneMonthAgo = DateTime.Now.AddMonths(-1); // Get the date one month ago

            // Filter transactions by type and date range (last 1 month)
            creditTransactions = allTransactions
                .Where(t => t.transactionType == TransactionType.credit && t.date >= oneMonthAgo)
                .ToList();
            debitTransactions = allTransactions
                .Where(t => t.transactionType == TransactionType.debit && t.date >= oneMonthAgo)
                .ToList();
            debtTransactions = allTransactions
                .Where(t => t.transactionType == TransactionType.debt && t.status == "unpaid" && t.date >= oneMonthAgo)
                .ToList();
        }
    }
}

