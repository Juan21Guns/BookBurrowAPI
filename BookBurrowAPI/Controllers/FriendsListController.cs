using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using ZstdSharp.Unsafe;

namespace BookBurrowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFriendsListRepository _friendAction;
        public FriendsListController(IMapper mapper, IFriendsListRepository friendAction)
        {
            _friendAction = friendAction;
            _mapper = mapper; 
        }

        [HttpGet("/friendstatus")]
        public IActionResult CheckFriendExists([FromQuery] int User1, [FromQuery] int User2)
        {
            var result = _friendAction.FriendConnectionExists(User1, User2);
            return result == null ? BadRequest("something went wrong") : Ok(result);


        }

        [HttpPost("/sendfriendrequest")]
        public IActionResult CreateFriend(int User1, int User2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please select a valid user");
            }

            var newFriend = _friendAction.CreateFriend(User1, User2);
            
            return newFriend == null ? StatusCode(StatusCodes.Status500InternalServerError) : Ok(newFriend);
            
        }

        [HttpPut("/updateRequest")]
        public IActionResult UpdateFriend(int Id, [FromBody] FriendsListDto friends)
        {
            if (friends == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != friends.User2)
            {
                return BadRequest("Only your friend can accept your request");
            }

            var mappedFriend = _mapper.Map<FriendsList>(friends);

            if (!_friendAction.UpdateFriend(mappedFriend))
            {
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpPut("/block")]
        public IActionResult BlockFriend(int User1, int User2)
        {
            return _friendAction.BlockFriend(User1, User2) == true ? Ok() : BadRequest("something went wrong");
        }

        [HttpDelete("/removefriend")]
        public IActionResult RemoveFriend(int Id, [FromBody] FriendsListDto friends)
        {
            if (friends == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != friends.User2 && Id != friends.User1)
            {
                return BadRequest("Only friends can break friends");
            }

            var mappedFriend = _mapper.Map<FriendsList>(friends);

            return _friendAction.RemoveFriend(mappedFriend) == true ? Ok() : BadRequest(); 
        }
    }
}
