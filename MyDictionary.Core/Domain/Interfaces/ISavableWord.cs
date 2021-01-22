using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Core.Domain.Interfaces
{
    public interface ISavableWord : IWord
    {
        bool IsSaved { get; set; }
    }
}
