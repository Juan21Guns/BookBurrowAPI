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
        public IActionResult GetUser([FromHeader] string sub)
        {
            var user = _mapper.Map<UsersDto>(_userAction.GetUser(sub));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("/byName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserByName(string? firstName, string? lastName)
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
        public IActionResult GetAllUsers(int startN, int endN, int friendStatus, string sub)
        {
            if ( endN == 0 || sub == "" )
            {
                return BadRequest();
            }
            var user = _mapper.Map<IList<UsersDto>>(_userAction.GetAllUsers(startN, sub, endN, friendStatus));
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("/createNewAccount")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult CreateNewUser([FromForm] string firstName, [FromForm] string lastName, [FromHeader] string sub)
        {
            //checks for if user exists will be done with Cognito at a later time

            bool didSave = _userAction.CreateUser(firstName, lastName, sub);

            if (!didSave)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("/updateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser([FromHeader] string sub, [FromBody] UsersDto newUser)
        {
            if (newUser == null)
            {
                return NotFound();
            }

/*            if (currentId != newUser.UserId)
            {
                return BadRequest("Does not match userId");
            }*/

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedUser = _mapper.Map<Users>(newUser);
            mappedUser.Sub = sub;

            if (!_userAction.UpdateUser(mappedUser))
            {
                return  StatusCode(StatusCodes.Status500InternalServerError);
            } else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpDelete("/deleteUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult RemoveUser([FromHeader] string sub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Check your input");
            }

            if (_userAction.UserExists(sub))
            {
                if (_userAction.DeleteUser(sub))
                {
                    return NoContent();
                }

                return BadRequest("Something went wrong with our servers");
            }

            return NotFound();
        }
    }
}
