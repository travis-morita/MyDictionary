using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using MyDictionary.Web.ViewModels;

namespace MyDictionary.Web.Controllers
{
    public class MyWordsController : BaseController
    {
        private readonly ILogger<MyWordsController> _logger;
        private readonly IUserWordRepository _userWordRepository;
        private readonly IUserWordService _userWordService;

        public MyWordsController(IUserWordService userWordService, IWordLookupRepository repository, IUserWordRepository userWordRepository, ILogger<MyWordsController> logger)
        {
            _userWordService = userWordService;
            _userWordRepository = userWordRepository;
            _logger = logger;
        }


        [HttpPost]
        public IActionResult Add(MyWordViewModel model)
        {
            if (ModelState.IsValid)
            {
                int userWordId = _userWordRepository.Create(
                    new UserWord
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Spelling = model.Word
                    });
            }

            return RedirectToAction("LookupWord", "Home", new { word = model.Word });
        }

        [HttpPost]
        public IActionResult Delete(string word)
        {
            if (ModelState.IsValid)
            {
                _userWordRepository.Delete(
                    new UserWord
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Spelling = word
                    });
            }

            return RedirectToAction("LookupWord", "Home", new { word = word });
        }

        [HttpGet]
        public IActionResult GetMyWords()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<UserWord> userWords = _userWordService.GetWordsByUserId(userId).ToList();

            return View("MyWords", userWords);
        }

        [HttpPost]
        public IActionResult Save(SaveWordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var wordDetailsList = model.Results.Where(m => m.IsSaved == true)
                    .Select(w => new WordDetails
                    {
                        Definition = w.Definition,
                        PartOfSpeech = w.PartOfSpeech,
                        Synonyms = w.Synonyms
                    }).ToList();

                int userWordId = _userWordRepository.Create(
                    new UserWord
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Spelling = model.Spelling,
                        WordDetails = wordDetailsList
                    });
            }

            return RedirectToAction("LookupWord", "Home", new { word = model.Spelling });
        }
    }
}