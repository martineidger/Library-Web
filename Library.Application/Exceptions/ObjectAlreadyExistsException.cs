using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException(string message) : base(message) { }
        public ObjectAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
