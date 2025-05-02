using System.Text.Json.Serialization;

namespace Linquiztic.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string WordText { get; set; } = string.Empty;
        public DateOnly AddedDate { get; set; }
        public string Mastery { get; set; } = string.Empty;
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
