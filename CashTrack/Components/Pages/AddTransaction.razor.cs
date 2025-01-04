using Microsoft.AspNetCore.Components;
using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;

namespace CashTrack.Pages
{
    public partial class AddTransaction : ComponentBase
    {
        private Transaction transaction = new Transaction { date = DateTime.Now };
        private List<Tag> tags = new List<Tag>();
        private bool showError = false;
        private string errorMessage = string.Empty;

        [Inject] public ITransactionService TransactionService { get; set; }
        [Inject] public ITagService TagService { get; set; }
        [Inject] public NavigationManager NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tags = await TagService.GetAllTags();
        }

        private async Task HandleValidSubmit()
        {
            var success = await TransactionService.AddTransaction(transaction);

            if (!success && transaction.transactionType == TransactionType.debit)
            {
                showError = true;
                errorMessage = "Insufficient balance for this debit transaction.";
            }
            else
            {
                NavManager.NavigateTo("/home");
            }
        }

        private void NavigateToAddTag()
        {
            NavManager.NavigateTo("/add-tag");
        }
    }
}
