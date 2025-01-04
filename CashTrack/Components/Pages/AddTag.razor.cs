using Microsoft.AspNetCore.Components;
using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;

namespace CashTrack.Pages
{
    public partial class AddTag
    {
        private string tagName;
        private bool isSuccess = false;
        private bool isError = false;

        [Inject] public ITagService TagService { get; set; }
        //[Inject] public NavigationManager Nav { get; set; }

        private async Task HandleValidSubmit()
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                isError = true;
                isSuccess = false;
                return;
            }

            var newTag = new Tag(tagName);
            var result = await TagService.AddTag(newTag);

            if (result)
            {
                isSuccess = true;
                isError = false;
                tagName = string.Empty;
            }
            else
            {
                isError = true;
                isSuccess = false;
            }
        }

        /*private void NavigateBack()
        {
            Nav.NavigateTo("/add-transaction");
        } */
    }
}


