using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBurrowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessagesRepository _messagesAction;
        private readonly IMapper _mapper;
        public MessagesController(IMessagesRepository messages, IMapper mapper)
        {
             _messagesAction = messages;
             _mapper = mapper;  
        }

        [HttpGet("/getMessages")]
        public IActionResult GetMessages(int cid, int start, int end)
        {
            if (cid == 0)
            {
                return NotFound();
            }

            var messages = _messagesAction.GetGroupMessages(cid, start, end);
            var mapped = _mapper.Map<ICollection<MessageDto>>(messages);

            if (mapped == null)
            {
                return BadRequest("could not get messages");
            }

            return Ok(mapped);
        }

        [HttpPost("/sendMessage")]
        public IActionResult SendMessage(MessageDto message)
        {
            if (!ModelState.IsValid || message == null)
            {
                return BadRequest("please send a valid message");
            }

            message.Edited = false;
            var mapped = _mapper.Map<Messages>(message);
            int ? sent = _messagesAction.SendMessage(mapped);

            if (sent.HasValue)
            {
                return Ok(sent.Value);
            }

            return BadRequest("something went wrong, please try again later");
        }

        [HttpPut("/updateMessage")]
        public IActionResult UpdateMessage(MessageDto message)
        {
            if (!ModelState.IsValid || message == null)
            {
                return BadRequest("please correct your message");
            }

            var mapped = _mapper.Map<Messages>(message);
            bool updated = _messagesAction.UpdateMessage(mapped);
            return updated ? Ok(message) : BadRequest("something went wrong with our servers");
        }

        [HttpDelete("/deleteMessage")]
        public IActionResult DeleteMessage(Messages message)
        {
            if (!ModelState.IsValid || message == null)
            {
                return BadRequest("please correct your request");
            }

            bool deleted = _messagesAction.DeleteMessage(message);

            return deleted ? Ok(message) : BadRequest("something went wrong with our servers");
        }
    }
}
