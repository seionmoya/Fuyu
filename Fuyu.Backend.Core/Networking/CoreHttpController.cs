using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.Core.Networking
{
    public abstract class CoreHttpController : HttpController
    {
        protected CoreHttpController(Regex pattern) : base(pattern)
        {
            // match dynamic paths
        }

        protected CoreHttpController(string path) : base(path)
        {
            // match static paths
        }

        public override Task RunAsync(HttpContext context)
        {
            var downcast = new CoreHttpContext(context.Request, context.Response);
            return RunAsync(downcast);
        }

        public abstract Task RunAsync(CoreHttpContext context);
    }

    public abstract class CoreHttpController<TRequest> : CoreHttpController where TRequest : class
    {
        protected CoreHttpController(Regex pattern) : base(pattern)
        {
            // match dynamic paths
        }

        protected CoreHttpController(string path) : base(path)
        {
            // match static paths
        }

        public override Task RunAsync(CoreHttpContext context)
        {
            // TODO:
            // - Use better exception type
            // -- seionmoya, 2024-10-13
            if (!context.HasBody())
            {
                throw new Exception("Request does not contain body.");
            }

            var body = context.GetJson<TRequest>();

            // TODO:
            // - Use better exception type
            // -- seionmoya, 2024-10-13
            if (body == null)
            {
                throw new Exception("Body could not be parsed as TRequest.");
            }

            return RunAsync(context, body);
        }

        public abstract Task RunAsync(CoreHttpContext context, TRequest body);
    }
}
