using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashTrack.DataModel.Model;

namespace CashTrack.Components.Pages
{
    public partial class AddTag
    {
        private string tagName = string.Empty;
        private string? successMessage = null;
        private string? errorMessage = null;

        // Method to handle adding a tag
        private async Task SubmitTag()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tagName))
                {
                    errorMessage = "Tag name cannot be empty.";
                    successMessage = null;
                    return;
                }

                var newTag = new Tag(tagName);
                await TagService.AddTag(newTag);

                successMessage = "Tag added successfully!";
                errorMessage = null;
                tagName = string.Empty; // Clear input after success
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred while adding the tag.";
                successMessage = null;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
