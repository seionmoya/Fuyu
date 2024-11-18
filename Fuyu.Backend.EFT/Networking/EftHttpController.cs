using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Networking
{
    public abstract class EftHttpController : HttpController
    {
        protected EftHttpController(Regex pattern) : base(pattern)
        {
            // match dynamic paths
        }

        protected EftHttpController(string path) : base(path)
        {
            // match static paths
        }

        public override Task RunAsync(HttpContext context)
        {
            // NOTE: assumes HttpController can be safely downcasted into EftHttpControler
            // -- seionmoya, 2024-11-18
            var downcast = (EftHttpContext)context;

            return RunAsync(downcast);
        }

        public abstract Task RunAsync(EftHttpContext context);
    }

    public abstract class EftHttpController<TRequest> : EftHttpController where TRequest : class
    {
        protected EftHttpController(Regex pattern) : base(pattern)
        {
            // match dynamic paths
        }

        protected EftHttpController(string path) : base(path)
        {
            // match static paths
        }

        public override Task RunAsync(EftHttpContext context)
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

        public abstract Task RunAsync(EftHttpContext context, TRequest body);
    }
}
