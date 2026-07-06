namespace Pan.Core
{
    public class WordForm
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public Word? Word { get; set; }
        public string Form { get; set; } = string.Empty;
        public string Morphology { get; set; } = string.Empty; // e.g. Genitive Singular Masculine, or Aorist Active Indicative 3S
    }
}
