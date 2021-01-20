using MyDictionary.Core.Domain;
using MyDictionary.Core.Domain.Interfaces;
using MyDictionary.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace MyDictionary.Infrastructure.Services
{
    public class UserWordService : IUserWordService
    {
        private IUserWordRepository _userWordRepo;
        private IWordLookupRepository _wordLookupRepo;
        private IWordRepository _wordRepository;

        public UserWordService(IUserWordRepository userWordRepo, IWordLookupRepository wordLookupRepo, IWordRepository wordRepository)
        {
            _userWordRepo = userWordRepo;
            _wordLookupRepo = wordLookupRepo;
            _wordRepository = wordRepository;
        }

        public MyWord GetWord(string word, string userId)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException("word cannot be empty");
            }

            if (String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId cannot be empty");
            }

            var myWord = new MyWord();

            myWord.Word = _wordRepository.GetWord(word);

            // rapid api call - depricated
            //ReturnWord wordResult = _wordLookupRepo.GetWord(word);
            //apiWord.Saved = _userWordRepo.GetUserWordId(userId, word) == 0 ? false : true;
            myWord.IsSaved = _userWordRepo.GetUserWordId(userId, word) == 0 ? false : true; 

            return myWord;


            
        }

        public IEnumerable<UserWord> GetWordsByUserId(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId cannot be empty");
            }

            return _userWordRepo.GetWordsByUserId(userId);
        }

        public int SaveUserWord(int userId, string word)
        {
            throw new NotImplementedException();
        }
    }
}
