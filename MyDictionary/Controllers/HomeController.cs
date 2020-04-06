using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using MyDictionary.Infrastructure.Repositories;
using MyDictionary.Models;
using MyDictionary.Web.Data.Mapping;
using MyDictionary.Web.ViewModels;
using Newtonsoft.Json;

namespace MyDictionary.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWordLookupRepository _repository;
        private readonly IUserWordRepository _userWordRepository;

        public HomeController(IWordLookupRepository repository, IUserWordRepository userWordRepository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _userWordRepository = userWordRepository;
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            var test = _userWordRepository.GetUserWord(new UserWord { UserId = 1, Spelling = "test" });
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LookupWord(string word)
        {
            //var word = HttpContext.Request.Form["word"][0];
            var wordLookup = await _repository.GetWord(word);
            var lookupWordViewModel = wordLookup.ToLookupWordViewModel();
            lookupWordViewModel.Results = wordLookup?.WordDetails?.ToResultViewModelList();

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

        [HttpPost]
        public IActionResult Save(LookupWordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var word = model.IsSaved;
            }

            return View("Home");
        }
    }
}
