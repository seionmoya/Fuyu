﻿using System.Threading.Tasks;
using Fuyu.Backend.Core.Models.Responses;
using Fuyu.Backend.Core.Networking;
using Fuyu.Backend.Core.Services;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core.Controllers;

public class AccountGamesController : AbstractCoreHttpController
{
    private readonly AccountService _accountService;

    public AccountGamesController() : base("/account/games")
    {
        _accountService = AccountService.Instance;
    }

    public override Task RunAsync(CoreHttpContext context)
    {
        var sessionId = context.SessionId;
        var result = _accountService.GetGames(sessionId);
        var response = new AccountGamesResponse()
        {
            Games = result
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text);
    }
}