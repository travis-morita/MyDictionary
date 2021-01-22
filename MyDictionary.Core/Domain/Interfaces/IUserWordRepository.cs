using MyDictionary.Core.Domain;
using System.Collections.Generic;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IUserWordRepository : IRepository<UserWord>
    {
        int GetUserWordId(string userId, string word);

        IEnumerable<UserWord> GetWordsByUserId(string userid);

    }
}
