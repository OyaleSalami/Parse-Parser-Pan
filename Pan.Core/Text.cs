using System;

namespace Pan.Core
{
    public class Text
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string GreekContent { get; set; } = string.Empty;
        public string EnglishTranslation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
