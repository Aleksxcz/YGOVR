using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YGOVR.Multiplayer
{
    public class MultiplayerWorldSession : MonoBehaviour
    {
        [Serializable]
        public class PlayerStateEvent : UnityEvent<MultiplayerPlayerState> { }

        [Serializable]
        public class SessionStateEvent : UnityEvent<string> { }

        [SerializeField] private string sessionCode;
        [SerializeField] private bool isHost;
        [SerializeField] private MultiplayerPlayerState localPlayer = new();
        [SerializeField] private List<MultiplayerPlayerState> connectedPlayers = new();
        [SerializeField] private SessionStateEvent onSessionChanged = new();
        [SerializeField] private PlayerStateEvent onPlayerJoined = new();
        [SerializeField] private PlayerStateEvent onPlayerLeft = new();

        public string SessionCode => sessionCode;
        public bool IsHost => isHost;
        public MultiplayerPlayerState LocalPlayer => localPlayer;
        public IReadOnlyList<MultiplayerPlayerState> ConnectedPlayers => connectedPlayers;

        public void HostSession(string playerId, string displayName)
        {
            sessionCode = GenerateSessionCode();
            isHost = true;
            connectedPlayers.Clear();
            localPlayer.Initialize(playerId, displayName);
            connectedPlayers.Add(localPlayer);
            onSessionChanged?.Invoke(sessionCode);
            onPlayerJoined?.Invoke(localPlayer);
        }

        public void JoinSession(string code, string playerId, string displayName)
        {
            sessionCode = string.IsNullOrWhiteSpace(code) ? sessionCode : code;
            isHost = false;
            localPlayer.Initialize(playerId, displayName);
            AddOrUpdatePlayer(localPlayer);
            onSessionChanged?.Invoke(sessionCode);
            onPlayerJoined?.Invoke(localPlayer);
        }

        public void LeaveSession(string playerId)
        {
            var player = GetPlayer(playerId);
            if (player == null)
            {
                return;
            }

            connectedPlayers.Remove(player);
            onPlayerLeft?.Invoke(player);

            if (connectedPlayers.Count == 0)
            {
                sessionCode = string.Empty;
                onSessionChanged?.Invoke(sessionCode);
            }
        }

        public bool SetPlayerReady(string playerId, bool ready)
        {
            var player = GetPlayer(playerId);
            if (player == null)
            {
                return false;
            }

            player.SetReady(ready);
            return true;
        }

        public bool SetPlayerSpectator(string playerId, bool spectator)
        {
            var player = GetPlayer(playerId);
            if (player == null)
            {
                return false;
            }

            player.SetSpectator(spectator);
            return true;
        }

        private void AddOrUpdatePlayer(MultiplayerPlayerState incomingPlayer)
        {
            var existing = GetPlayer(incomingPlayer.PlayerId);
            if (existing != null)
            {
                connectedPlayers.Remove(existing);
            }

            connectedPlayers.Add(incomingPlayer);
        }

        private MultiplayerPlayerState GetPlayer(string playerId)
        {
            if (string.IsNullOrWhiteSpace(playerId))
            {
                return null;
            }

            foreach (var player in connectedPlayers)
            {
                if (string.Equals(player.PlayerId, playerId, StringComparison.OrdinalIgnoreCase))
                {
                    return player;
                }
            }

            return null;
        }

        private string GenerateSessionCode()
        {
            const string characters = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var codeCharacters = new char[6];

            for (var index = 0; index < codeCharacters.Length; index++)
            {
                codeCharacters[index] = characters[UnityEngine.Random.Range(0, characters.Length)];
            }

            return new string(codeCharacters);
        }
    }
}
