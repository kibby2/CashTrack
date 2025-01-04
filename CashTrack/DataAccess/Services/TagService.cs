using CashTrack.DataModel.Model;
using CashTrack.DataAccess.Services.Interface;
using System.Text.Json;

namespace CashTrack.DataAccess.Services
{
    public class TagService : ITagService
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tags.json");
        private List<Tag> _tags;

        public TagService()
        {
            _tags = LoadTags();
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await Task.FromResult(_tags);
        }

        public async Task<bool> AddTag(Tag tag)
        {
            if (_tags.Any(t => t.TagName.Equals(tag.TagName, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Tag with the same name already exists
            }

            _tags.Add(tag);
            SaveTags(_tags);
            return await Task.FromResult(true);
        }

        private List<Tag> LoadTags()
        {
            if (!File.Exists(filePath))
            {
                return new List<Tag>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Tag>>(json) ?? new List<Tag>();
        }

        private void SaveTags(List<Tag> tags)
        {
            var json = JsonSerializer.Serialize(tags);
            File.WriteAllText(filePath, json);
        }
    }
}
