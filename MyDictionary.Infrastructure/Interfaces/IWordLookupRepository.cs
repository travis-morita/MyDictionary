using System.Threading.Tasks;
using MyDictionary.Core.Domain;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IWordLookupRepository
    {
        Task<WordLookup> GetWord(string word);
        Task<string> GetSearchResult(string searchString);
    }
}
