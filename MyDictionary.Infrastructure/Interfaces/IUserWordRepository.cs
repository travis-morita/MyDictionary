using MyDictionary.Core.Domain;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IUserWordRepository : IRepository<UserWord>
    {
        UserWord GetUserWord(UserWord userWord);

        
    }
}
