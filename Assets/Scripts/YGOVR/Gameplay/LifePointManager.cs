using System;
using UnityEngine;
using UnityEngine.Events;

namespace YGOVR.Gameplay
{
    public class LifePointManager : MonoBehaviour
    {
        [Serializable]
        public class LifePointsChangedEvent : UnityEvent<string, int> { }

        [SerializeField] private DuelPlayerState playerOne = new();
        [SerializeField] private DuelPlayerState playerTwo = new();
        [SerializeField] private LifePointsChangedEvent onLifePointsChanged = new();

        public DuelPlayerState PlayerOne => playerOne;
        public DuelPlayerState PlayerTwo => playerTwo;
        public LifePointsChangedEvent OnLifePointsChanged => onLifePointsChanged;

        private void Awake()
        {
            playerOne.ResetLifePoints();
            playerTwo.ResetLifePoints();
            NotifyLifePointsChanged(playerOne);
            NotifyLifePointsChanged(playerTwo);
        }

        public bool ApplyDamage(string playerId, int amount)
        {
            return UpdateLifePoints(playerId, amount, true);
        }

        public bool RestoreLifePoints(string playerId, int amount)
        {
            return UpdateLifePoints(playerId, amount, false);
        }

        private bool UpdateLifePoints(string playerId, int amount, bool asDamage)
        {
            if (string.IsNullOrWhiteSpace(playerId))
            {
                return false;
            }

            var player = GetPlayer(playerId);
            if (player == null)
            {
                return false;
            }

            if (asDamage)
            {
                player.ApplyDamage(amount);
            }
            else
            {
                player.RestoreLifePoints(amount);
            }

            NotifyLifePointsChanged(player);
            return true;
        }

        private DuelPlayerState GetPlayer(string playerId)
        {
            if (string.Equals(playerOne.PlayerId, playerId, StringComparison.OrdinalIgnoreCase))
            {
                return playerOne;
            }

            if (string.Equals(playerTwo.PlayerId, playerId, StringComparison.OrdinalIgnoreCase))
            {
                return playerTwo;
            }

            return null;
        }

        private void NotifyLifePointsChanged(DuelPlayerState playerState)
        {
            if (playerState == null)
            {
                return;
            }

            onLifePointsChanged?.Invoke(playerState.PlayerId, playerState.CurrentLifePoints);
        }
    }
}
