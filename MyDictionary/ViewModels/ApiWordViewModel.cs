using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.ViewModels
{
    public class ApiWordViewModel
    {
        public IEnumerable<ApiWordMetaViewModel> ApiWordMeta { get; set; }
        public bool IsSaved { get; set; }
    }
}
