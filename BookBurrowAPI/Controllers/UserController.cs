using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Mapping;
using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBurrowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userAction;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userAction, IMapper mapper)
        {
            _userAction = userAction; 
            _mapper = mapper;
        }

        [HttpGet("/one")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int Id)
        {
            Console.WriteLine(Id);
            var user = _mapper.Map<UsersDto>(_userAction.GetUser(Id));
            Console.WriteLine($"User is {user}");
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("/all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers(int Id)
        {
            Console.WriteLine(Id);
            var user = _mapper.Map<IList<UsersDto>>(_userAction.GetAllUsers(Id));
            Console.WriteLine($"User is {user}");
            return user == null ? NotFound() : Ok(user);
        }
    }
}
