
using MyDictionary.Core.Domain.Interfaces;

namespace MyDictionary.Core.Domain
{
    public class Pronunciation : IPronunciation
    {
        public string All { get; set; }
        public string Text { get; set; }
    }
}
