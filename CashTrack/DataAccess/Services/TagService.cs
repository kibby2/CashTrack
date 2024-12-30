using CashTrack.DataAccess.Services.Interface;
using CashTrack.DataModel.Model;

namespace CashTrack.DataAccess.Services
{
    public class TagService : TagBase, ITagService
    {
        public async Task<List<Tag>> GetAllTags()
        {
            
            var tags = LoadTags();
            return await Task.FromResult(tags);
        }
        public async Task<bool> AddTag(Tag tag)
        {
            var tags = LoadTags();  

            
            if (tags.Any(t => t.TagName.Equals(tag.TagName, StringComparison.OrdinalIgnoreCase)))
            {
                return false;  
            }
           
            tags.Add(tag);

            SaveTags(tags);

            return true;  
        }
    }
}

