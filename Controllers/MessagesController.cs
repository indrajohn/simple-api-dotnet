using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MessagesApp.Utils;

namespace MessagesApp.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly MessageRepository _repository = new();

        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var messages = _repository.GetMessages();
            if (!messages.Any())
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.NoMessagesFound, StatusCodes.Status404NotFound);
            }
            return ResponseUtil.SuccessResponse(messages, SuccessMessages.MessagesRetrievedSuccessfully);
        }

        [HttpGet("{id}")]
        public IActionResult GetMessageById(int id)
        {
            if (id <= 0)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.InvalidIdMustPositive);
            }

            var message = _repository.GetMessageById(id);
            if (message == null)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.NoMessageWithId(id), StatusCodes.Status404NotFound);
            }

            return ResponseUtil.SuccessResponse(message, SuccessMessages.MessagesWithIdRetrievedSuccessfully(id));
        }

        [HttpPost]
        public IActionResult AddMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.InvalidMessageData);
            }
            if (message.Id <= 0)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.InvalidIdMustPositive);
            }

            var existingMessage = _repository.GetMessageById(message.Id);
            if (existingMessage != null)
            {
                return ResponseUtil.ErrorResponse(string.Format(ErrorMessages.MessageWithIdAlreadyExists(message.Id), message.Id));
            }

            _repository.AddMessage(message);
            return ResponseUtil.SuccessResponse(message, SuccessMessages.MessageAddedSuccessfully);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            if (id <= 0)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.InvalidIdMustPositive);
            }

            bool deleted = _repository.DeleteMessage(id);
            if (!deleted)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.NoMessageWithId(id), StatusCodes.Status404NotFound);
            }
            return ResponseUtil.SuccessResponse(null, SuccessMessages.MessageDeletedSuccessfully);
        }

    }
}
