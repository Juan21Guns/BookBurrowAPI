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

        [HttpGet]
        public IActionResult GetUsers(bool all)
        {
            var user = _mapper.Map<List<UsersDto>>(_userAction.GetUser(all));
            return Ok(user);
        }
    }
}
