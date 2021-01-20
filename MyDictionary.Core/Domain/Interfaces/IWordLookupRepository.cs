using System.Threading.Tasks;
using MyDictionary.Core.Domain;
using MyDictionary.Core.Domain.Interfaces;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IWordLookupRepository
    {
        ReturnWord GetWord(string word);
        Task<string> GetSearchResult(string searchString);
    }
}
