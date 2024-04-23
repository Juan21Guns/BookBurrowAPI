using AutoMapper;
using BookBurrowAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            int result = _friendAction.FriendConnectionExists(User1, User2);
            return result == -1 ? BadRequest("No friendship found </3") : Ok(result);


        }
    }
}
