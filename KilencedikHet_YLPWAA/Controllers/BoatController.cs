using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilencedikHet_YLPWAA.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BoatController : ControllerBase
    {
        [HttpGet]
        [Route("hajo/kerdesek")]

        public IActionResult függvénynév()
        {
            Models.HajosContext hajosContext = new Models.HajosContext();
            var lisa = from x in hajosContext.Questions select x;
            return Ok(lisa); // automatikusan JSON-né fog konvertálódni, nem kell vele foglalkozni
        }

        [HttpGet]
        [Route("hajo/kerdesek/{id}")]

        public IActionResult f2(int id)
        {
            Models.HajosContext hajosContext = new Models.HajosContext();
            var lisa = from x in hajosContext.Questions
                       where x.QuestionId == id
                       select x;
            
            // var lisa2 = hajosContext.Questions.Where(x => x.QuestionId == id); ugyanez lenne

            return Ok(lisa);
        }
    }
}
