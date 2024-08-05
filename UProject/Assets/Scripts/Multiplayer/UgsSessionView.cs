using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace STak4.brickout
{
    public class UgsSessionView : MonoBehaviour, IDisposable
    {
        public TMP_Text NameText;
        public TMP_Text PlayerCountText;
        public Button JoinButton;
        public Lobby Lobby;

        public UnityEvent<Lobby> OnJoinClick = new UnityEvent<Lobby>();

        private void Start()
        {
            JoinButton.onClick.AddListener(OnJoin);
        }

        private void Update()
        {
            PlayerCountText.text = $"{Lobby.Players.Count}/{Lobby.MaxPlayers}";
        }

        public void Dispose()
        {
            JoinButton.onClick.RemoveListener(OnJoin);
            Destroy(gameObject);
        }

        private void OnJoin()
        {
            OnJoinClick?.Invoke(Lobby);
        }
    }
}
