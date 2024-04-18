using BookBurrowAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBurrowAPI.Controllers
{
    [ApiController]
    public class TestController
    {
        private readonly ITestSQL _repo;
        public TestController(ITestSQL repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("/home")]
        public void TestThis()
        {
            _repo.GetTest();
        }
    }
}
