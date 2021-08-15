using System.Collections.Generic;

namespace Hedwig.Handler
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message) : base("000002", message)
        {
        }
    }
}
