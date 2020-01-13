
namespace MyDictionary.Core.Domain
{
    public class Result
    {
        public string Definition { get; set; }

        public string PartOfSpeech { get; set; }

        public string[] Synonyms { get; set; }

        public string[] SimilarTo { get; set; }

        public string[] Derivation { get; set; }

        public string[] Examples { get; set; }
    }
}
