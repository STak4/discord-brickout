using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace STak4.brickout.Multiplayer
{
    public class UgsSessionCreator
    {
        private UgsSessionInfo _info;
        public UgsSessionCreator(UgsSessionInfo info)
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                Debug.LogError($"[UGS] Not Initialized!");
                return;
            }

            _info = info;
        }

        public UniTask<Lobby> CreateSession(CancellationToken token = default)
        {
            return LobbyService.Instance
                .CreateLobbyAsync(_info.GetSessionName(), _info.GetMaxPlayers(), _info.GetOptions()).AsUniTask()
                .AttachExternalCancellation(cancellationToken: token);
        }
    }
}
