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

        [HttpPost("/createGroup")]
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
            string message = "";
            var mappedUsers = _mapper.Map<ICollection<PGUserNames>>(userNames);
            foreach (var pgu in mappedUsers)
            {
                try
                {
                    pgu.Username = "";
                    _groupAction.AddUsers(pgu);
                } catch (Exception ex)
                {
                    message += ex;
                }
            }
            return Ok(message);
        }

        [HttpPut("/updateGroup")]
        public IActionResult UpdateGroup(PrivateGroupsDto info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Make sure your input is valid");
            }

            var mappedInfo = _mapper.Map<PrivateGroups>(info);
            return _groupAction.UpdateGroup(mappedInfo) == true ? Ok() : BadRequest(); 
        }

        [HttpPut("/updateUserName")]
        public IActionResult UpdateUserName(PGUserNamesDto name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Make sure your input is valid");
            }

            var mappedInfo = _mapper.Map<PGUserNames>(name);
            return _groupAction.UpdateUserName(mappedInfo) == true ? Ok() : BadRequest();
        }

        [HttpDelete("/deleteGroupChat")]
        public IActionResult DeleteGroup(int id)
        {
            if (id != 0)
            {
                return _groupAction.DeleteGroup(id) == true ? Ok() : BadRequest();
            }

            return NotFound("Group not found");
        }

        [HttpDelete("/deleteUserFromGroup")]
        public IActionResult DeleteUser(PGUserNamesDto user)
        {
            if (user == null)
            {
                return NotFound("User not found");
            }

            var mappedUser = _mapper.Map<PGUserNames>(user);
            return _groupAction.DeleteUserName(mappedUser) == true ? Ok() : BadRequest();
        }
    }
}
