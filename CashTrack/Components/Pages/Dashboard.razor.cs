using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace CashTrack.Pages
{
    public partial class Dashboard : ComponentBase
    {
        private List<Transaction> paidDebts = new List<Transaction>();

        [Inject] public ITransactionService TransactionService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var allTransactions = await TransactionService.GetAllTransactions();
            paidDebts = allTransactions.Where(t => t.transactionType == TransactionType.debt && t.status == "Paid").ToList();
        }

        private async Task RemovePaidDebt(Guid transactionId)
        {
            var success = await TransactionService.DeleteTransaction(transactionId);

            if (success)
            {
                // Refresh the list after removing a debt
                var allTransactions = await TransactionService.GetAllTransactions();
                paidDebts = allTransactions.Where(t => t.transactionType == TransactionType.debt && t.status == "Paid").ToList();
            }
            else
            {
                // Handle failure (e.g., transaction not found)
                Console.WriteLine("Failed to remove paid debt.");
            }
        }
    }
}
