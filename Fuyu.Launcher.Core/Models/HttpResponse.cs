using System;

namespace Fuyu.Launcher.Core.Models
{
    public class HttpResponse<T>
        where T : class
    {
        public T Response;
        public Exception Error;
    }
}