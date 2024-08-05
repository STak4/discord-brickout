using System;
using System.Linq;
using MackySoft.Navigathena.SceneManagement;
using STak4.brickout.Multiplayer;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using UnityEngine;
using VContainer.Unity;

namespace STak4.brickout.Presenter
{
    public class InGamePresenter : IStartable
    {
        private InGameView _view;
        public InGamePresenter(InGameView view)
        {
            _view = view;
        }
        
        public void Start()
        {
            _view.backButton.onClick.AddListener(OnBack);
        }

        private async void OnBack()
        {
            var sessions = await LobbyService.Instance.GetJoinedLobbiesAsync();
            if (sessions.Any())
            {
                try
                {
                    var session = await LobbyService.Instance.GetLobbyAsync(sessions[0]);
                    var leaver = new UgsSessionLeaver(session);
                    await leaver.Leave();
                    Debug.Log($"[InGame]Leave Session");
                }
                catch (Exception e)
                {
                    Debug.LogError($"[InGame] Failed to leave Session. {e.Message}");
                    throw;
                }
            }

            await GlobalSceneNavigator.Instance.Pop();
        }
    }
}
