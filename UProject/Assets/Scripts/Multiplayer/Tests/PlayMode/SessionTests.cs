using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace STak4.brickout.Multiplayer.Tests.PlayMode
{
    public class SessionTests
    {
        public readonly string ProfileName = "tester1";
        private bool _setupComplete = false;
        public bool SetupComplete => _setupComplete;
        
        [SetUp]
        public async void Setup()
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                var initializer = new UgsInitializer(new UgsEnvironment(), new UgsProfile(ProfileName));
                await initializer.InitializeTask();
            }
            
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                var auth = new UgsAuth();
                await auth.SignIn();
            }
            await UniTask.WaitUntil(() => AuthenticationService.Instance.IsSignedIn);
            _setupComplete = true;
        }
        
        [TearDown]
        public async void TearDown()
        {
            _setupComplete = false;
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

            if (AuthenticationService.Instance.IsSignedIn)
            {
                AuthenticationService.Instance.SignOut();
            }
        }
        
        [Test]
        public async Task CreatePass()
        {
            await UniTask.WaitUntil(() => AuthenticationService.Instance.IsSignedIn);

            var session = await CreateSession();
            Assert.IsTrue(session != null);
        }

        // TODO: 一人で部屋の作成＆参加を行うには？
        // [Test]
        // public async Task JoinPass()
        // {
        //     await UniTask.WaitUntil(() => _setupComplete);
        //     // 事前に部屋を作らないといけない
        //     var session = await CreateSession();
        //     var leaver = new UgsSessionLeaver(session);
        //     await leaver.Leave();
        //     
        //     // 事前に作った部屋にジョイン
        //     var joiner = new UgsSessionJoiner(session);
        //     await joiner.JoinLobby();
        //     var joined = await LobbyService.Instance.GetJoinedLobbiesAsync();
        //     Assert.That(joined.Count > 0);
        // }

        [Test]
        public async Task LeavePass()
        {
            await UniTask.WaitUntil(() => AuthenticationService.Instance.IsSignedIn);
            
            // 事前に部屋を作り参加する
            var session = await CreateSession();
            // TODO: Rate limit問題
            // var joined = await LobbyService.Instance.GetJoinedLobbiesAsync();
            Assert.That(session.Players.Count > 0);
            
            var leaver = new UgsSessionLeaver(session);
            await leaver.Leave();
            //var joined = await LobbyService.Instance.GetJoinedLobbiesAsync();
            try
            {
                session = await LobbyService.Instance.GetLobbyAsync(session.Id);
                Assert.That(false);
            }
            catch (Exception e)
            {
                Debug.Log($"[TEST][Leave]OK. {e.Message}");
                Assert.That(true);
            }
        }

        private async UniTask<Lobby> CreateSession()
        {
            var options = new CreateLobbyOptions()
            {
                IsLocked = false,
                IsPrivate = false
            };
            
            var info = new UgsSessionInfo(new Guid().ToString(), options);
            var creator = new UgsSessionCreator(info);
            
            return await creator.CreateSession();
        }
    }
}
