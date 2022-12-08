using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KilencedikHet_YLPWAA.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BoatController : ControllerBase
    {
        [HttpGet]
        [Route("hajo/kerdesek/all")]

        public IActionResult függvénynév()
        {
            Models.HajosContext hajosContext = new Models.HajosContext();
            var lisa = from x in hajosContext.Questions select x.Question1;
            return Ok(lisa); // automatikusan JSON-né fog konvertálódni, nem kell vele foglalkozni
        }

        [HttpGet]
        [Route("hajo/kerdesek/{id}")]

        public IActionResult f2(int id)
        {
            Models.HajosContext hajosContext = new Models.HajosContext();
            var kérdés = (from x in hajosContext.Questions
                       where x.QuestionId == id
                       select x).FirstOrDefault();

            // var lisa2 = hajosContext.Questions.Where(x => x.QuestionId == id); ugyanez lenne

            if (kérdés == null) return BadRequest("Nincs ilyen sorszámú kérdés");

            return new JsonResult(kérdés);
        }

        [HttpGet]
        [Route("questions/count")]
        public int M4() //Tetszőleges metódusnév
        {
            Models.HajosContext context = new Models.HajosContext();
            int kérdésekSzáma = context.Questions.Count();

            return kérdésekSzáma;
        }
    }
}
