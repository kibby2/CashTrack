using CashTrack.DataModel.Model;
using System.Threading.Tasks;

namespace CashTrack.DataAccess.Services.Interface
{
    public interface ITagService
    {
        Task AddTag(Tag tag);
        Task<List<Tag>> GetAllTags();
    }
}
