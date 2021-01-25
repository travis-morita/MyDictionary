using MyDictionary.Core.Domain.Interfaces;
using System;

namespace MyDictionary.Core.Domain
{
    public class UserWord : Word
    {
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
