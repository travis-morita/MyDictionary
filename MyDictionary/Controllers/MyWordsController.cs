using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Add(string word)
        {
            if (ModelState.IsValid)
            {
                _userWordRepository.Create(
                    new UserWord
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Spelling = word
                    });
            }

            return RedirectToAction("GetWord", "Home", new { word });
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

            return RedirectToAction("GetWord", new { word });
        }

        [HttpPost]
        public IActionResult DeleteFromList(string id)
        {
            if (ModelState.IsValid)
            {
                _userWordRepository.Delete(
                    new UserWord
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Spelling = id
                    });
            }

            return RedirectToAction("GetWords");
        }

        [HttpGet]
        public IActionResult GetWords()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<UserWord> userWords = _userWordService.GetWordsByUserId(userId).ToList();

            return View("MyWords", userWords);
        }

        //[HttpGet]
        //public IActionResult GetWordsJson()
        //{
        //    //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    List<UserWord> userWords = _userWordService.GetWordsByUserId("0603f9ea-0223-49b8-9a2b-9ae52de89521").ToList();

        //    return new JsonResult(userWords);
        //}

        [HttpGet]
        public IActionResult GetWordsJson(string id)
        {
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<UserWord> userWords = _userWordService.GetWordsByUserId(id).ToList();

            return new JsonResult(userWords);
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