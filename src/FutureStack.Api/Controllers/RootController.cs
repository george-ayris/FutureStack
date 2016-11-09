using Microsoft.AspNetCore.Mvc;

namespace FutureStack.Api.Controllers
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
