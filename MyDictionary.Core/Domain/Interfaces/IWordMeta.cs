using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Core.Domain.Interfaces
{
    public interface IWordMeta
    {
        IList<string> Definitions { get; set; }
    }
}
