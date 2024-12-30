using CashTrack.DataModel.Model;

namespace CashTrack.DataAccess.Services.Interface
{
    public interface IUserService
    {
        Task<bool> Login(User user);
    }
}
