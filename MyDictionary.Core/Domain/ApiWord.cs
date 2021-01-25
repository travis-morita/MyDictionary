using MyDictionary.Core.Domain.Interfaces;
using System.Collections.Generic;

namespace MyDictionary.Core.Domain
{
    public class ApiWord : IWord
    {
        public string Id { get; set; }

        public string Syllables { get; set; }
        
        public string PartOfSpeech { get; set; }
        
        public IEnumerable<string> Definitions { get; set; }
        
        public IEnumerable<string> Pronunciations { get; set; }
    }
}
