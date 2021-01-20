using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Infrastructure.Interfaces;
using MyDictionary.Models;
using MyDictionary.Web.Data.Mapping;
using MyDictionary.Web.ViewModels;

namespace MyDictionary.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserWordRepository _userWordRepository;
        private readonly IUserWordService _userWordService;
        private readonly IWordRepository _wordRepository;

        public HomeController(IUserWordService userWordService, IUserWordRepository userWordRepository, IWordRepository wordRepository, ILogger<HomeController> logger)
        {
            _userWordService = userWordService;
            _userWordRepository = userWordRepository;
            _wordRepository = wordRepository;
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var test = _userWordRepository.GetUserWordId(uid, "word");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult LookupWord(string word)
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var wordLookup = _userWordService.GetWord(word, userId);
        //    LookupWordViewModel lookupWordViewModel = null;

        //    if (wordLookup != null)
        //    {
        //        lookupWordViewModel = wordLookup.ToLookupWordViewModel();
        //        lookupWordViewModel.Results = wordLookup?.WordDetails?.ToResultViewModelList();
        //        lookupWordViewModel.UserId = userId;
        //        lookupWordViewModel.IsSaved = wordLookup.Saved;
        //    }


        //    return View("Index", lookupWordViewModel);
        //}

        [HttpGet]
        public IActionResult GetWord(string word)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wordResults = _userWordService.GetWord(word, userId);
            
            if (wordResults == null)
            {
                return View();
            }

            var apiWordViewModel = new ApiWordViewModel
            {
                ApiWordMeta = wordResults.Word.Select(w => new ApiWordMetaViewModel
                {
                    Spelling = w.Id,
                    PartOfSpeech = w.PartOfSpeech,
                    Syllables = w.Syllables,
                    WordDisplay = (w.Id.IndexOf(":") > 0 ? w.Id.Substring(0, w.Id.IndexOf(":")) : w.Id),
                    Definitions = w.Definitions,
                    Pronunciation = w.Pronunciations?.ToList()[0].Text
                }).Where(w => w.WordDisplay == word),
                IsSaved = _userWordRepository.GetUserWordId(userId, word) == 0 ? false : true
            };

            return View(apiWordViewModel);
        }

        [AllowAnonymous]
        public IActionResult GetWordJson(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var apiWordMeta = _userWordService.GetWord(id, userId);
            //var apiWordMeta = _userWordService.GetWord(id, "463a14b7-de25-48e0-8705-fbaac83134e8");

            var apiWordViewModel = new ApiWordViewModel
            {
                ApiWordMeta = apiWordMeta.Word.Select(w => new ApiWordMetaViewModel
                {
                    Spelling = w.Id,
                    PartOfSpeech = w.PartOfSpeech,
                    Syllables = w.Syllables,
                    WordDisplay = w.Id, // (w.Id.IndexOf(":") > 0 ? w.Id.Substring(0, w.Id.IndexOf(":")) : w.Id),
                    Definitions = w.Definitions,
                    Pronunciation = w.Pronunciations?.ToList()[0].Text
                }), //.Where(w => w.WordDisplay == id),
                IsSaved = _userWordRepository.GetUserWordId(userId, id) == 0 ? false : true
            };

            return new JsonResult(apiWordMeta);
        }

        
        [HttpGet]
        public async Task<string> StringSearch(string searchString)
        {
            var searchResults = await _wordRepository.GetSearchResult(searchString);
            return searchResults;
            //return JsonConvert.SerializeObject(searchResults.Results.data);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> StringSearchJson(string id)
        {
            var searchResults = await _wordRepository.GetSearchResult(id);
            return new JsonResult(searchResults);
            //return JsonConvert.SerializeObject(searchResults.Results.data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}
