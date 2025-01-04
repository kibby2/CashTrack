using CashTrack.DataModel.Model;

namespace CashTrack.DataAccess.Services.Interface
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTags();
        Task<bool> AddTag(Tag tag);
    }
}
