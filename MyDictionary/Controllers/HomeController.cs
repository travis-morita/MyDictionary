using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Core.Domain;
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
        private readonly IWordLookupRepository _repository;
        private readonly IUserWordRepository _userWordRepository;
        private readonly IUserWordService _userWordService;

        public HomeController(IUserWordService userWordService, IWordLookupRepository repository, IUserWordRepository userWordRepository, ILogger<HomeController> logger)
        {
            _userWordService = userWordService;
            _repository = repository;
            _userWordRepository = userWordRepository;
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

        [HttpGet]
        public IActionResult LookupWord(string word)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wordLookup = _userWordService.GetWord(word, userId);
            var lookupWordViewModel = wordLookup.ToLookupWordViewModel();
            lookupWordViewModel.Results = wordLookup?.WordDetails?.ToResultViewModelList();
            lookupWordViewModel.UserId = userId;
            lookupWordViewModel.IsSaved = wordLookup.Saved;

            return View("Index", lookupWordViewModel);
        }


        public async Task<string> StringSearch(string searchString)
        {
            var searchResults = await _repository.GetSearchResult(searchString);
            return searchResults;
            //return JsonConvert.SerializeObject(searchResults.Results.data);
         }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}
