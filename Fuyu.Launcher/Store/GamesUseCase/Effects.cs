using System.Threading.Tasks;
using Fluxor;
using Fuyu.Launcher.Core.Services;

namespace Fuyu.Launcher.Store.GamesUseCase
{
    public class Effects
    {
        [EffectMethod]
        public Task HandleGetGamesAction(GetGamesAction action, IDispatcher dispatcher)
        {
            var games = RequestService.GetGames();
            dispatcher.Dispatch(new GetGamesResultAction(games));
            return Task.CompletedTask;
        }
    }
}
