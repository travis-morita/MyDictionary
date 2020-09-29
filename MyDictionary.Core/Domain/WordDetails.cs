
using System.Collections.Generic;

namespace MyDictionary.Core.Domain
{
    public class WordDetails
    {
        public string Definition { get; set; }

        public string PartOfSpeech { get; set; }

        public List<string> Synonyms { get; set; }

        public string[] SimilarTo { get; set; }

        public string[] Derivation { get; set; }

        public string[] Examples { get; set; }
    }
}
