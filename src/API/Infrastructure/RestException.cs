using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Infrastructure
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; }
        public RestException(HttpStatusCode code, object errors = null)
        {
            this.Errors = errors;
            this.Code = code;
        }
    }
}