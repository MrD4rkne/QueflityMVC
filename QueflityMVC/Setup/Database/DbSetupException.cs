using System.Runtime.Serialization;

namespace QueflityMVC.Web.Setup.Database
{
    public class DbSetupException : Exception
    {
        public DbSetupException()
        {
        }

        public DbSetupException(string? message) : base(message)
        {
        }

        public DbSetupException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DbSetupException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}