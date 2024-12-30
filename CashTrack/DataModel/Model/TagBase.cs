using System.Text.Json;

namespace CashTrack.DataModel.Model
{
    public abstract class TagBase
    {

        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "tags.json");
        protected List<Tag> LoadTags()
        {
            if (!File.Exists(FilePath)) return new List<Tag>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Tag>>(json) ?? new List<Tag>();
        }
        protected void SaveTags(List<Tag> tags)
        {
            var json = JsonSerializer.Serialize(tags);
            File.WriteAllText(FilePath, json);
        }
    }
}

