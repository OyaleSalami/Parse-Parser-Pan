using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Pan.Core;

namespace Pan.Editor
{
    public interface IGreekTextParser
    {
        string NormalizeGreek(string text);
        List<TextToken> Tokenize(string text, HashSet<string> knownForms);
    }

    public class TextToken
    {
        public string RawValue { get; set; } = string.Empty;
        public string NormalizedValue { get; set; } = string.Empty;
        public bool IsWord { get; set; }
        public bool IsHighlighted { get; set; }
    }

    public class GreekTextParser : IGreekTextParser
    {
        // Unicode categories for diacritic markings
        public string NormalizeGreek(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // FormD decomposes characters into base characters and combining characters
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                // Keep only letter characters and digits (non-spacing marks are diacritics)
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Convert to lowercase for case-insensitive matching
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
        }

        public List<TextToken> Tokenize(string text, HashSet<string> knownForms)
        {
            var tokens = new List<TextToken>();
            if (string.IsNullOrEmpty(text))
                return tokens;

            // Simple regex separating words (Greek and English letters) from punctuation/whitespace
            // Match consecutive letters/numbers, or match single non-alphanumeric characters
            var matches = Regex.Matches(text, @"([\p{L}\p{N}]+)|([^\p{L}\p{N}])");

            foreach (Match match in matches)
            {
                string value = match.Value;
                bool isWord = Regex.IsMatch(value, @"[\p{L}]");
                string normalized = isWord ? NormalizeGreek(value) : value;

                bool isHighlighted = false;
                if (isWord && knownForms != null)
                {
                    isHighlighted = knownForms.Contains(normalized);
                }

                tokens.Add(new TextToken
                {
                    RawValue = value,
                    NormalizedValue = normalized,
                    IsWord = isWord,
                    IsHighlighted = isHighlighted
                });
            }

            return tokens;
        }
    }
}
