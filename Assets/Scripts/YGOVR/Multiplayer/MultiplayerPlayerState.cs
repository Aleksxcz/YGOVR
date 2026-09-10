using System;
using UnityEngine;

namespace YGOVR.Multiplayer
{
    [Serializable]
    public class MultiplayerPlayerState
    {
        [SerializeField] private string playerId;
        [SerializeField] private string displayName;
        [SerializeField] private bool isReady;
        [SerializeField] private bool isSpectator;

        public string PlayerId => playerId;
        public string DisplayName => displayName;
        public bool IsReady => isReady;
        public bool IsSpectator => isSpectator;

        public void Initialize(string id, string name)
        {
            playerId = id;
            displayName = name;
            isReady = false;
            isSpectator = false;
        }

        public void SetReady(bool ready)
        {
            isReady = ready;
        }

        public void SetSpectator(bool spectator)
        {
            isSpectator = spectator;
        }
    }
}
