using System.Threading.Tasks;
using Fluxor;
using Fuyu.Launcher.Core.Services;
using Microsoft.AspNetCore.Components;

namespace Fuyu.Launcher.Store.GamesUseCase;

public class Effects
{
    [EffectMethod]
    public Task HandleGetGamesAction(GetGamesAction action, IDispatcher dispatcher)
    {
        var httpResponse = RequestService.GetGames();

        dispatcher.Dispatch(new GetGamesResultAction(httpResponse.Response.Games));
        return Task.CompletedTask;
    }
}