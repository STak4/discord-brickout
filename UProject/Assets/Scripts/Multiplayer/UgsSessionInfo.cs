using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies;
using UnityEngine;

namespace STak4.brickout.Multiplayer
{
    public class UgsSessionInfo
    {
        private string _name;
        private int _maxPlayers = 4;
        private CreateLobbyOptions _options;
        public UgsSessionInfo(string name, CreateLobbyOptions options, int players = 4)
        {
            _name = name;
            _options = options;
            _maxPlayers = players;
        }

        public string GetSessionName()
        {
            return _name;
        }

        public int GetMaxPlayers()
        {
            return _maxPlayers;
        }

        public CreateLobbyOptions GetOptions()
        {
            return _options;
        }
    }
}
