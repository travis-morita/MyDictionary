using MyDictionary.Core.Domain;
using MyDictionary.Core.Domain.Interfaces;
using System.Collections.Generic;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IUserWordService
    {
        MyWord GetWord(string word, string userId);
        //ReturnWord GetWord(string word, string userId);
        int SaveUserWord(int userId, string word);
        IEnumerable<UserWord> GetWordsByUserId(string userId);
    }
}
