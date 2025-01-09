using System;

namespace Fuyu.Backend.Core.Networking;

public class RequestNoBodyException : Exception
{
    public RequestNoBodyException(string message) : base(message)
    {

    }
}