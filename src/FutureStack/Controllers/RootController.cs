using Microsoft.AspNetCore.Mvc;

namespace FutureStack.Controllers
{
    [Route("/")]
    [Route("api")]
    public class RootController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "Hello yiannis";
        }

    }
}
