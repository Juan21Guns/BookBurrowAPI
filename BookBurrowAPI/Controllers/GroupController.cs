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

        [HttpGet("/getByName")]
        public IActionResult GetByName(string groupName)
        {
            var groups = _groupAction.FindGroupsByName(groupName);
            return groups == null ? NotFound() : Ok(groups);
        }

        [HttpGet("/findGroup")]
        public IActionResult GetGroup(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please select a valid id");
            }
            var mappedResult = _mapper.Map<PrivateGroupsDto>(_groupAction.GetGroup(id));
            return mappedResult == null ? NotFound() : Ok(mappedResult);
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

            return NotFound("Something went wrong");
        }

        [HttpPost("/CreateGroup")]
        public IActionResult CreateGroup([FromForm] PrivateGroupsDto names)
        {
            var mappedNames = _mapper.Map<PrivateGroups>(names);
            var result = _groupAction.CreateGroup(mappedNames);

            if (result == -1)
            {
                return BadRequest("Something went wrong");
            } else if (result == 0)
            {
                return NotFound("Please check content");
            }

            return Ok(result);
        }

        [HttpPost("/addMembers")]
        public IActionResult AddMembers(ICollection<PGUserNamesDto> userNames)
        {
            var mappedUsers = _mapper.Map<ICollection<PGUserNames>>(userNames);
            foreach (var pgu in mappedUsers)
            {
                Console.WriteLine("this is working");
                try
                {
                    _groupAction.AddUsers(pgu);
                } catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            return Ok();
        }
    }
}
