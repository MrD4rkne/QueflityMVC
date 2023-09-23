using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Errors.Identity
{
    public class UserCannotManageThemselveException : Exception
    {
        private const string DEFAULT_MESSAGE = "User cannot manage their's identity configuration";
        public UserCannotManageThemselveException() : this(DEFAULT_MESSAGE)
        {
        }

        public UserCannotManageThemselveException(string? message) : base(message)
        {
        }

        public UserCannotManageThemselveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserCannotManageThemselveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
