using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Exceptions.UseCases;
public class NoCategoriesException : Exception
{
    public NoCategoriesException()
    {
    }

    public NoCategoriesException(string? message) : base(message)
    {
    }

    public NoCategoriesException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
