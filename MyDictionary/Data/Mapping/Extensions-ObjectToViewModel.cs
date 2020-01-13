using System;
using System.Collections.Generic;
using MyDictionary.Core.Domain;
using MyDictionary.Web.ViewModels;

namespace MyDictionary.Web.Data.Mapping
{
    public static class Extensions_ObjectToViewModel
    {
        public static LookupWordViewModel ToLookupWordViewModel(this WordLookup wordLookup)
        {
            return new LookupWordViewModel(wordLookup.Word, wordLookup.Syllables, wordLookup.Pronunciation);
        }

        public static List<ResultViewModel> ToResultViewModelList(this Result[] results)
        {
            var resultsList = new List<ResultViewModel>();
            foreach(Result result in results)
            {
                resultsList.Add(new ResultViewModel { Definition = result.Definition, PartOfSpeech = result.PartOfSpeech, Synonyms = result.Synonyms });
            }

            return resultsList;
        }
    }
}
