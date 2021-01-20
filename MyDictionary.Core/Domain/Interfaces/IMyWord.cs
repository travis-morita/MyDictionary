using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Core.Domain.Interfaces
{
    public interface IMyWord 
    {
        bool IsSaved { get; set; }
        IEnumerable<IWord> Word { get; set; }
    }
}
