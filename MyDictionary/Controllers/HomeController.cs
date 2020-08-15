using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var test = _userWordRepository.GetUserWord(new UserWord { UserId = "", Spelling = "test" });
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
            lookupWordViewModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

                //List<WordDetails> wordDetailsList = new List<WordDetails>();

                //foreach (var result in model.Results)
                //{
                //    if (result.IsSaved)
                //    {
                //        var wordDetails = new WordDetails
                //        {
                //            Definition = result.Definition,
                //            PartOfSpeech = result.PartOfSpeech,
                //            Synonyms = result.Synonyms
                //        };

                //        wordDetailsList.Add(wordDetails);
                //    }  
                //}

                var userWordId = _userWordRepository.Create(
                    new UserWord { 
                        UserId = model.UserId, 
                        Spelling = model.Spelling,
                        WordDetails = wordDetailsList 
                    });
            }

            return RedirectToAction("Home");
        }
    }
}
