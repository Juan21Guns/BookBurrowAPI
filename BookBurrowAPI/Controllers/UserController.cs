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
        public IActionResult GetUser([FromQuery] int Id)
        {
            var user = _mapper.Map<UsersDto>(_userAction.GetUser(Id));
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("/byName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var user = _mapper.Map<ICollection<UsersDto>>(_userAction.GetUserByName(firstName, lastName));
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("/all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers(int startN, int endN, int friendId, int friendStatus)
        {
            var user = _mapper.Map<IList<UsersDto>>(_userAction.GetAllUsers(startN, endN, friendId, friendStatus));
            return user == null ? NotFound() : Ok(user);
        }
    }
}
