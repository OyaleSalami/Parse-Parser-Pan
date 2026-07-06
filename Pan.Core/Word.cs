using System.Collections.Generic;

namespace Pan.Core
{
    public class Word
    {
        public int Id { get; set; }
        public string GreekLemma { get; set; } = string.Empty;
        public PartOfSpeech PartOfSpeech { get; set; }
        public string DefinitionEn { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public ICollection<WordForm> WordForms { get; set; } = new List<WordForm>();
    }
}
