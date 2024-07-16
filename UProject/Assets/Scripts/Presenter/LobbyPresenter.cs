using System;
using System.Collections.Generic;
using MackySoft.Navigathena.SceneManagement;
using STak4.brickout.Multiplayer;
using STak4.brickout.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using VContainer.Unity;

namespace STak4.brickout.Presenter
{
    public class LobbyPresenter : IStartable
    {
        private LobbyView _view;
        private UgsSessionFinder _finder;

        private List<UgsSessionView> _sessionViews = new List<UgsSessionView>();
        public LobbyPresenter(LobbyView view)
        {
            _view = view;
            _finder = new UgsSessionFinder();
        }

        public void Start()
        {
            _view.NextButton.onClick.AddListener(OnNext);
            _view.CreateButton.onClick.AddListener(OnCreate);
            StartPollSessions();
        }

        private void StartPollSessions()
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;

            // Filter for open lobbies only
            options.Filters = new List<QueryFilter>()
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0")
            };

            // Order by newest lobbies first
            options.Order = new List<QueryOrder>()
            {
                new QueryOrder(
                    asc: false,
                    field: QueryOrder.FieldOptions.Created)
            };

            // ポーリング開始
            _finder.PollForFind(options, OnSessionUpdated).Forget();
        }

        /// <summary>
        /// Lobby(Session）更新時
        /// </summary>
        /// <param name="lobbies"></param>
        private void OnSessionUpdated(List<Lobby> lobbies)
        {
            // TODO: ObjectPool利用
            // 削除
            for (int i = _sessionViews.Count - 1; i >= 0; i--) {
                _sessionViews[i].Dispose();
                _sessionViews.RemoveAt(i);
            }

            _sessionViews = new List<UgsSessionView>();
            // 生成
            foreach (var l in lobbies)
            {
                var session = GameObject.Instantiate(_view.SessionViewPrefab, _view.SessionViewParent);
                session.Lobby = l;
                session.OnJoinClick.AddListener(OnJoin);
                _sessionViews.Add(session);                
            }
        }

        private async void OnNext()
        {
            // ポーリング終了
            _finder.Dispose();
            
            var identifier = new BuiltInSceneIdentifier("InGame");
            await GlobalSceneNavigator.Instance.Push(identifier);
        }

        private async void OnCreate()
        {
            var options = new CreateLobbyOptions()
            {
                IsLocked = false,
                IsPrivate = false
            };
            
            var info = new UgsSessionInfo(new Guid().ToString(), options);
            var creator = new UgsSessionCreator(info);
            var session = await creator.CreateSession();
            Debug.Log($"[UGS][Lobby] Session created. lobby:{session.Name}, id:{session.Id}");
            OnNext();
        }

        private async void OnJoin(Lobby lobby)
        {
            var joiner = new UgsSessionJoiner(lobby);
            try
            {
                await joiner.JoinLobby();
                Debug.Log($"[UGS][Lobby] Session joined. lobby:{lobby.Name}, id:{lobby.Id}");
                OnNext();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
