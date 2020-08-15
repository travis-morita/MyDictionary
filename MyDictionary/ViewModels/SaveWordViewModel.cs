using System.Collections.Generic;

namespace MyDictionary.Web.ViewModels
{
    public class SaveWordViewModel
    {
        public string UserId { get; set; }
        public string Spelling { get; set; }
        public string WordDisplay { get; set; }
        public string Pronunciation { get; set; }
        public List<ResultViewModel> Results { get; set; }
    }
}
