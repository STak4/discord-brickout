using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace STak4.brickout.Multiplayer
{
    public class UgsSessionFinder : IDisposable
    {
        private QueryResponse _lastResponse;
        private CancellationTokenSource _tokenSource;
        public UgsSessionFinder()
        {
            _tokenSource = new CancellationTokenSource();
        }

        public async UniTaskVoid PollForFind(QueryLobbiesOptions options, Action<List<Lobby>> onSessionUpdated, float pollIntervalSeconds = 3.0f)
        {
            while (!_tokenSource.Token.IsCancellationRequested)
            {
                var response = await Lobbies.Instance.QueryLobbiesAsync(options);
                if (_lastResponse == null || response.Results != _lastResponse.Results)
                {
                    onSessionUpdated?.Invoke(response.Results);
                }

                _lastResponse = response;
                Debug.Log($"[Debug][UGS][LobbyFinder] Get Lobbies. Count:{response.Results.Count}");
                await UniTask.WaitForSeconds(pollIntervalSeconds);
            }
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
        }
    }
}
