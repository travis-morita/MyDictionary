using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace MyDictionary.Infrastructure.Services
{
    public class UserWordService : IUserWordService
    {
        private IUserWordRepository _userWordRepo;
        private IWordLookupRepository _wordLookupRepo;

        public UserWordService(IUserWordRepository userWordRepo, IWordLookupRepository wordLookupRepo)
        {
            _userWordRepo = userWordRepo;
            _wordLookupRepo = wordLookupRepo;
        }

        public ApiWord GetWord(string word, string userId)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException("word cannot be empty");
            }

            if (String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId cannot be empty");
            }

            try
            {
                ApiWord wordResult = _wordLookupRepo.GetWord(word);
                wordResult.Saved = _userWordRepo.GetUserWordId(userId, word) == 0 ? false : true;

                return wordResult;
            }
            catch (Exception ex)
            {

            }

            return null;
            
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
