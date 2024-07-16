using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace STak4.brickout.Multiplayer
{
    public class UgsSessionLeaver
    {
        private Lobby _lobby;
        public UgsSessionLeaver(Lobby lobby)
        {
            _lobby = lobby;
        }

        public UniTask Leave(CancellationToken token = default)
        {
            //Ensure you sign-in before calling Authentication Instance
            //See IAuthenticationService interface
            string playerId = AuthenticationService.Instance.PlayerId;
            return LobbyService.Instance.RemovePlayerAsync(_lobby.Id, playerId).AsUniTask().AttachExternalCancellation(token);
        }
    }
}
