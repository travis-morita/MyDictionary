using Newtonsoft.Json;

namespace MyDictionary.Core.Domain
{
    public class Word
    {

        [JsonProperty("Word")]
        public string Spelling { get; set; }

        [JsonProperty("Results")]
        public WordDetails[] WordDetails { get; set; }

        public Syllables Syllables { get; set; }

        public double Frequency { get; set; }

        public Pronunciation Pronunciation { get; set; }
    }
}
