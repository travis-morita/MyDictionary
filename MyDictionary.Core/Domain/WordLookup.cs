using Newtonsoft.Json;

namespace MyDictionary.Core.Domain
{
    public class WordLookup
    {
        public string Word { get; set; }

        public Result[] Results { get; set; }

        public Syllables Syllables { get; set; }

        public double Frequency { get; set; }

        public Pronunciation Pronunciation { get; set; }
    }
}
