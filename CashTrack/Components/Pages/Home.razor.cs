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
    }
}

