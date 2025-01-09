using System;

namespace Fuyu.Backend.Core.Networking;

public class RequestBodyNotParsableException : Exception
{
    public RequestBodyNotParsableException(string message) : base(message)
    {

    }
}