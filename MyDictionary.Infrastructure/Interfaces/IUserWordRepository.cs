using MyDictionary.Core.Domain;
using System.Threading.Tasks;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IUserWordRepository
    {
        Task<UserWord> GetUserWord(string word);
    }
}
