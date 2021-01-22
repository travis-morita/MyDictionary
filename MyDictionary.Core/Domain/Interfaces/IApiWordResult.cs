using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Core.Domain.Interfaces
{
    public interface IApiWordResult
    {
        IEnumerable<IWord> Results { get; set; }
        string Error { get; set; }
    }
}
