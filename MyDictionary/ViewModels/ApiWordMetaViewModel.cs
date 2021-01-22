using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.ViewModels
{
    public class ApiWordMetaViewModel
    {

        public string UserId { get; set; }
        public string Spelling { get; set; } 
        public string Syllables { get; set; }
        public string WordDisplay { get; set; }
        public IEnumerable<string> Pronunciations { get; set; }
        public IEnumerable<string> Definitions { get; set; }
        public string PartOfSpeech { get; set; }
        public List<string> Synonyms { get; set; }
        public int Number { get; set; }
        public bool IsSaved { get; set; }
    }
}
