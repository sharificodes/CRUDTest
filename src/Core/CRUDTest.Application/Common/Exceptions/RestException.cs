using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Common.Exceptions;

public class RestException : Exception
{
    public RestException(HttpStatusCode code, object message = default)
    {
        Code = code;
        Message = message;
    }

    public HttpStatusCode Code { get; init; }

    public new object Message { get; init; }
}

