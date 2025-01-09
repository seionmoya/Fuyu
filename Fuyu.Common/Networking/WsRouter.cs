using System.Threading.Tasks;

namespace Fuyu.Common.Networking;

public class WsRouter : Router<WsController, WsContext>
{
    public WsRouter() : base()
    {
    }

    public override async Task RouteAsync(WsContext context)
    {
        var matches = GetAllMatching(context);
        var tasks = new Task[matches.Count];
        for (var i = 0; i < matches.Count; i++)
        {
            tasks[i] = matches[i].RunAsync(context);
        }

        await Task.WhenAll(tasks);

        while (context.IsOpen())
        {
            await context.PollAsync();
        }

        // NOTE: No need to call context.CloseAsync here
        // because ReceiveAsync will handle that for us
        // -- nexus4880, 2024-10-23
    }
}