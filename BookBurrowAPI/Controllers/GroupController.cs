using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Mapping;
using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBurrowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupAction;
        public GroupController(IGroupRepository groupAction, IMapper mapper)
        {
            _groupAction = groupAction;
            _mapper = mapper;
        }

        [HttpGet("/findGroup")]
        public IActionResult GetGroup(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please select a valid id");
            }
            var mappedResult = _mapper.Map<PrivateGroupsDto>(_groupAction.GetGroup(id));
            return Ok(mappedResult);
        }

        [HttpGet("/loadGroupUsers")]
        public IActionResult GetGroupUsers(int id)
        {
            if (id == 0)
            {
                return NotFound("Please select a valid id");
            }

            var joinedUsers = _groupAction.GetGroupUsers(id);

            if (joinedUsers != null)
            {
                return Ok(joinedUsers);
            }
            
            return BadRequest("Something went wrong");
        }
    }
}
