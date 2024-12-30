using System;
using CashTrack.DataModel.Model;

namespace CashTrack.DataAccess.Services.Interface
{
    public interface ITagService
    {
        Task<bool> AddTag(Tag tag);
        Task<List<Tag>> GetAllTags();
    }
}
