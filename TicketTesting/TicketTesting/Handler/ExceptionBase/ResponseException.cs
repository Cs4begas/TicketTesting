using System.Collections.Generic;

namespace Hedwig.Handler
{
    public class ResponseException 
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
