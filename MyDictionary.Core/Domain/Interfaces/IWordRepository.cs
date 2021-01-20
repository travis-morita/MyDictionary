using MyDictionary.Core.Domain;
using MyDictionary.Core.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IWordRepository
    {
        
        IEnumerable<IWord> GetWord(string word);
        Task<string> GetSearchResult(string searchString);
    
    }
}
