using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagesApp.Utils
{
    public static class ErrorMessages
    {
        public const string MessageCannotBeNull = "Message cannot be null.";
        public const string MessageMustContainIdAndText = "Message must contain both an id and text.";
        public const string InvalidIdMustPositive = "Invalid ID. Please provide a valid positive integer ID";
        public const string NoMessagesFound = "No messages found.";
        public const string InvalidMessageData = "Invalid message data. Please check your input and try again.";
        public static string MessageWithIdAlreadyExists(string id)
        {
            return $"A message with id {id} already exists.";
        }
         public static string NoMessageWithId(string id)
        {
            return $"No message found with id {id}.";
        }
    }

    public static class SuccessMessages
    {
        public const string MessageAddedSuccessfully = "Message added successfully.";
        public const string MessagesRetrievedSuccessfully = "Messages retrieved successfully.";
        public const string  MessageDeletedSuccessfully = "Message deleted successfully.";
        public const string  MessageUpdatedSuccessfully = "Message updated successfully.";

        public static string MessagesWithIdRetrievedSuccessfully(string id)
        {
            return $"Messages with id {id} retrieved successfully";
        }
        
    }

    public static class ResponseUtil
    {
        public static IActionResult SuccessResponse(object data, string message = null)
        {
            var response = new
            {
                status = "success",
                data = data,
                message = message
            };
            return new OkObjectResult(response);
        }

        public static IActionResult ErrorResponse(string message, int statusCode = StatusCodes.Status400BadRequest)
        {
            var response = new { status = "error", message = message };
            return new ObjectResult(response) { StatusCode = statusCode };
        }
    }
}
