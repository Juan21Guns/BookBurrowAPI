using BookBurrowAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBurrowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestDBString : Controller
    {
        private readonly IGetConnectionString _connectPls;
        public TestDBString(IGetConnectionString connectPls)
        {
            _connectPls = connectPls;
        }

        [HttpGet(Name = "GetLine")]
        public void getMethod()
        {
            string s = _connectPls.nameOfString();
            Console.WriteLine(s);
        }
    }
}
