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

            // Filter transactions by type
            creditTransactions = allTransactions.Where(t => t.transactionType == TransactionType.credit).ToList();
            debitTransactions = allTransactions.Where(t => t.transactionType == TransactionType.debit).ToList();
            debtTransactions = allTransactions.Where(t => t.transactionType == TransactionType.debt).ToList();
        }
    }
}
