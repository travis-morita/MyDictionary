using System;
using MyDictionary.Core.Domain;

namespace MyDictionary.Web.ViewModels
{
    public class ResultViewModel
    {
        public string PartOfSpeech { get; set; }
        public string Definition { get; set; }
        public string[] Synonyms { get; set; }
        public bool IsSaved { get; set; }
    }
}
