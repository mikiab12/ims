using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) : base(message)
        {
        }
    }

    public class UsernameException : Exception
    {
        public UsernameException(string message) : base(message)
        {

        }
    }

    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException(string exception) : base(exception)
        {

        }
    }
}
