using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Errors.UseCases;
public class ItemIsPartOfKitException : Exception
{
    public ItemIsPartOfKitException()
    {
    }

    public ItemIsPartOfKitException(string? message) : base(message)
    {
    }

    public ItemIsPartOfKitException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
