using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Infrastructure.Interfaces;
using MyDictionary.Models;
using MyDictionary.Web.ViewModels;

namespace MyDictionary.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserWordRepository _userWordRepository;
        private readonly IApiWordRepository _wordRepository;

        public HomeController(IUserWordRepository userWordRepository, IApiWordRepository wordRepository, ILogger<HomeController> logger)
        {
            _userWordRepository = userWordRepository;
            _wordRepository = wordRepository;
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetWord(string word)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wordApiResult = _wordRepository.GetWord(word);
            
            if (wordApiResult == null)
            {
                return View();
            }

            if (!string.IsNullOrEmpty(wordApiResult.Error))
            {
                return View(new ApiWordViewModel { ApiError = wordApiResult.Error });
            }

            var apiWordViewModel = new ApiWordViewModel
            {
                ApiWordMeta = wordApiResult.Results.Select(w => new ApiWordMetaViewModel
                {
                    Spelling = w.Id,
                    PartOfSpeech = w.PartOfSpeech,
                    Syllables = w.Syllables,
                    WordDisplay = w.Id, //(w.Id.IndexOf(":") > 0 ? w.Id.Substring(0, w.Id.IndexOf(":")) : w.Id),
                    Definitions = w.Definitions,
                    Pronunciations = w.Pronunciations
                }).Where(w => w.WordDisplay.ToLower() == word.ToLower()),
                IsSaved = _userWordRepository.GetUserWordId(userId, word) == 0 ? false : true
            };

            return View("Index", apiWordViewModel);
        }

        [AllowAnonymous]
        public IActionResult GetWordJson(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var apiWordMeta = _wordRepository.GetWord(id);
            //var apiWordMeta = _userWordService.GetWord(id, "463a14b7-de25-48e0-8705-fbaac83134e8");

            var apiWordViewModel = new ApiWordViewModel
            {
                ApiWordMeta = apiWordMeta.Results.Select(w => new ApiWordMetaViewModel
                {
                    Spelling = w.Id,
                    PartOfSpeech = w.PartOfSpeech,
                    Syllables = w.Syllables,
                    WordDisplay = w.Id, // (w.Id.IndexOf(":") > 0 ? w.Id.Substring(0, w.Id.IndexOf(":")) : w.Id),
                    Definitions = w.Definitions,
                    Pronunciations = w.Pronunciations
                }).Where(w => w.WordDisplay.ToLower() == id.ToLower()),
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
