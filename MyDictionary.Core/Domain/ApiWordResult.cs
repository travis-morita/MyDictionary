using MyDictionary.Core.Domain.Interfaces;
using System.Collections.Generic;

namespace MyDictionary.Core.Domain
{
    public class ApiWordResult : IApiWordResult
    {
        public IEnumerable<IWord> Results { get; set; }
        public string Error { get; set; }
        public bool NotFound { get; set; }
    }
}
