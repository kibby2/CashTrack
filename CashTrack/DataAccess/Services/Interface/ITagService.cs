using CashTrack.DataModel.Model;

namespace CashTrack.DataAccess.Services.Interface
{
    public interface ITagService
    {
        Task AddTag(Tag tag);
        Task<List<Tag>> GetAllTags();
    }
}
