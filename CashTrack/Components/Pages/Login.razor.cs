using CashTrack.DataModel.Model;

namespace CashTrack.Components.Pages
{
    public partial class Login
    {
        private string? ErrorMessage;

        public User Users { get; set; } = new();

        #region Login
        private async void HandleLogin()
        {
            if (await UserService.Login(Users))
            {
                Nav.NavigateTo("/home");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }
        #endregion

    }
}
