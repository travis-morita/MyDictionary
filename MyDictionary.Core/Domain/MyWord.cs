using MyDictionary.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Core.Domain
{
    public class MyWord : ISavableWord
    {
        public string Id { get; set; }
        public string Syllables { get; set; }
        public string PartOfSpeech { get; set; }
        public IEnumerable<string> Definitions { get; set; }
        public IEnumerable<string> Pronunciations { get; set; }
        public bool IsSaved { get; set; }
    }
}
