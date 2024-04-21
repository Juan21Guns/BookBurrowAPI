using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Mapping;
using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("/byName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            if (firstName == null && lastName == null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<ICollection<UsersDto>>(_userAction.GetUserByName(firstName, lastName));
            if (user.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("/all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers(int startN, int endN, int friendId, int friendStatus)
        {
            if ( endN == 0 || friendId == 0 )
            {
                return BadRequest();
            }
            var user = _mapper.Map<IList<UsersDto>>(_userAction.GetAllUsers(startN, endN, friendId, friendStatus));
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("/createNewAccount")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult CreateNewUser([FromForm] string firstName, [FromForm] string lastName)
        {
            //checks for if user exists will be done with Cognito at a later time

            bool didSave = _userAction.CreateUser(firstName, lastName);

            if (!didSave)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("/updateUser")]
        [ProducesResponseType(201)]
        public IActionResult UpdateUser(int currentId, [FromBody] UsersDto newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }

            if (_userAction.GetUser(currentId).UserId == 0)
            {
                return NotFound();
            }

            var mappedUser = _mapper.Map<Users>(newUser);
            var endResult = _userAction.UpdateUser(currentId, mappedUser);

            if (endResult == false)
            {
                return  StatusCode(StatusCodes.Status500InternalServerError);
            } else
            {
                return Ok();
            }
        }
    }
}
