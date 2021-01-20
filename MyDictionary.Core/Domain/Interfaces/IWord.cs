using System.Collections.Generic;
using System.Collections;

namespace MyDictionary.Core.Domain.Interfaces
{
    public interface IWord
    {
        string Id { get; set; }
        string Syllables { get; set; }
        string PartOfSpeech { get; set; }
        IEnumerable<string> Definitions { get; set; }
        IEnumerable<IPronunciation> Pronunciations { get; set; }
    }
}
