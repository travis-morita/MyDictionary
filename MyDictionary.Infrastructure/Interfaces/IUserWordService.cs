using MyDictionary.Core.Domain;
using System.Collections.Generic;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IUserWordService
    {
        ApiWord GetWord(string word, string userId);
        int SaveUserWord(int userId, string word);
        IEnumerable<UserWord> GetWordsByUserId(string userId);
    }
}
