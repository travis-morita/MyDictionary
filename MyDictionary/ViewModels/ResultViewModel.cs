using System;
using System.Collections.Generic;
using MyDictionary.Core.Domain;

namespace MyDictionary.Web.ViewModels
{
    public class ResultViewModel
    {
        public string PartOfSpeech { get; set; }
        public string Definition { get; set; }
        public List<string> Synonyms { get; set; }
        public bool IsSaved { get; set; }
    }
}
