using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;

namespace MyDictionary.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserWordService _userWordService;
        private readonly IUserWordRepository _userWordRepository;

        public HomeController(IUserWordService userWordService, IUserWordRepository userWordRepository, ILogger<HomeController> logger)
        {
            _userWordService = userWordService;
            _userWordRepository = userWordRepository;
            _logger = logger;
        }

       
        public IActionResult GetWord(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var apiWordMeta = _userWordService.GetWord(id, userId);
            //var apiWordMeta = _userWordService.GetWord(id, "463a14b7-de25-48e0-8705-fbaac83134e8");

   

            return new JsonResult(apiWordMeta);
        }


        [HttpPost]
        public IActionResult Add([FromBody] string text)
        {

            if (ModelState.IsValid)
            {
                return new JsonResult(_userWordRepository.Create(
                    new UserWord
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Spelling = text
                    })
                );
            }

            return null;
        }


        [HttpDelete, Route("Home/Delete/{id}")]
        public IActionResult Delete(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            
            _userWordRepository.Delete(
                new UserWord
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Spelling = id
                });
            

            return NoContent();
        }

    }
}
