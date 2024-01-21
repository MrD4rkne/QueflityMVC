using System.Runtime.Serialization;
using System.Text;

namespace QueflityMVC.Application.Errors.Common
{
    public class EntityNotFoundException : Exception
    {
        private const string ERROR_MESSAGE_SCHEME = "with this id could not be found.";
        private static readonly string DEFAULT_ERROR_MESSAGE = $"Entity {ERROR_MESSAGE_SCHEME}";

        public EntityNotFoundException() : this(DEFAULT_ERROR_MESSAGE)
        {
        }

        public EntityNotFoundException(string? entityName = "", string? message = "") : base(GetMessageWithEntityName(entityName, message))
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GetMessageWithEntityName(string? entityName, string? message)
        {
            entityName = entityName ?? string.Empty;
            entityName!.Trim();

            message = message ?? string.Empty;
            message!.Trim();

            if (string.IsNullOrEmpty(entityName))
            {
                return DEFAULT_ERROR_MESSAGE;
            }
            StringBuilder errorMessageBuilder = new();
            errorMessageBuilder.Append(entityName);

            if (string.IsNullOrEmpty(message))
            {
                errorMessageBuilder.Append(ERROR_MESSAGE_SCHEME);
            }
            else
            {
                errorMessageBuilder.Append(message);
            }

            string errorMessage = errorMessageBuilder.ToString();
            return errorMessage;
        }
    }
}