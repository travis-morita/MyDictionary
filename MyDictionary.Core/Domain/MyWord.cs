using MyDictionary.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Core.Domain
{
    public class MyWord : IMyWord
    {
        public bool IsSaved { get; set; }

        public IEnumerable<IWord>  Word { get; set; }
    }
}
