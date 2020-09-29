using System.Collections.Generic;

namespace MyDictionary.Core.Domain.Interfaces
{
    public abstract class Word
    {
        public string Spelling { get; set; }
        public Syllables Syllables { get; set; }
        public Pronunciation Pronunciation { get; set; }
        public IList<WordDetails> WordDetails { get; set; }
        public bool Saved { get; set; }
    }
}
