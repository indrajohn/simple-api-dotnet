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
        public IActionResult GetMessageById(string id)
        {

            var message = _repository.GetMessageById(id);
            if (message == null)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.NoMessageWithId(id), StatusCodes.Status404NotFound);
            }

            return ResponseUtil.SuccessResponse(message, SuccessMessages.MessagesWithIdRetrievedSuccessfully(id));
        }

        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            Guid newUuid = Guid.NewGuid();
            string uuidString = newUuid.ToString();
            _logger.LogInformation("UUID {uuidString}", uuidString);

            message.Id = uuidString;

            if (!ModelState.IsValid)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.InvalidMessageData);
            }

            var existingMessage = _repository.GetMessageById(message.Id);
            if (existingMessage != null)
            {
                return ResponseUtil.ErrorResponse(string.Format(ErrorMessages.MessageWithIdAlreadyExists(message.Id), message.Id));
            }

            _repository.AddMessage(message);
            return ResponseUtil.SuccessResponse(message, SuccessMessages.MessageAddedSuccessfully);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMessage(string id, [FromBody] Message updatedMessage)
        {
            var existingMessage = _repository.GetMessageById(id);
            if (existingMessage == null)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.NoMessageWithId(id), StatusCodes.Status404NotFound);
            }
            existingMessage.Text = updatedMessage.Text; 
            _repository.UpdateMessage(existingMessage); 
            return ResponseUtil.SuccessResponse(existingMessage, SuccessMessages.MessageUpdatedSuccessfully);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(string id)
        {

            bool deleted = _repository.DeleteMessage(id);
            if (!deleted)
            {
                return ResponseUtil.ErrorResponse(ErrorMessages.NoMessageWithId(id), StatusCodes.Status404NotFound);
            }
            return ResponseUtil.SuccessResponse(null, SuccessMessages.MessageDeletedSuccessfully);
        }

    }
}
