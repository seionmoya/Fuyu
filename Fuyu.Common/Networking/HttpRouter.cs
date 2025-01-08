namespace Fuyu.Common.Networking
{
    public class HttpRouter : Router<AbstractHttpController, HttpContext>
    {
        public HttpRouter() : base()
        {
        }
    }
}