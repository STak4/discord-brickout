using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace STak4.brickout.Multiplayer
{
    public class UgsSessionJoiner
    {
        private Lobby _lobby;

        public UgsSessionJoiner(Lobby lobby)
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                Debug.LogError($"[UGS] Not Initialized!");
                return;
            }
            
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError($"[UGS] Failed to Join. not signed in");
                return;
            }
            _lobby = lobby;
        }

        public UniTask<Lobby> JoinLobby(CancellationToken token = default)
        {
            return LobbyService.Instance.JoinLobbyByIdAsync(_lobby.Id).AsUniTask().AttachExternalCancellation(token);
        }
    }
}
