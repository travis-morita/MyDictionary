using System;
using System.Collections.Generic;
using MyDictionary.Core.Domain;

namespace MyDictionary.Web.ViewModels
{
    public class LookupWordViewModel
    {
        private string _spelling;
        private string _wordDisplay;
        private string _syllableDisplay;
        private string _pronunciation;
       
        public LookupWordViewModel()
        { }

        public LookupWordViewModel(string spelling, Syllables syllables, Pronunciation pronunciation)
        {
            _spelling = spelling;
            _wordDisplay  = spelling;

            if (syllables?.List?.Length > 0)
            {
                foreach (var syllable in syllables.List)
                {
                    _syllableDisplay += syllable + "·";
                }
                _syllableDisplay = _syllableDisplay[0..^1];
            }

            _pronunciation = pronunciation?.All != null ? $"/ {pronunciation.All} / " : "";

        }


        public string UserId { get; set; }
        public string Spelling { get => _spelling; set { _spelling = value; } }
        public string WordDisplay { get => _syllableDisplay ?? _wordDisplay; }
        public string Pronunciation { get => _pronunciation; }
        public List<ResultViewModel> Results { get; set; }
        public string[] Synonyms { get; set; }
        
    }
}
