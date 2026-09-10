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
        [SerializeField] private string defaultPlayerOneId = "PlayerOne";
        [SerializeField] private string defaultPlayerTwoId = "PlayerTwo";
        [SerializeField] private int defaultStartingLifePoints = 8000;
        [SerializeField] private LifePointsChangedEvent onLifePointsChanged = new();

        public DuelPlayerState PlayerOne => playerOne;
        public DuelPlayerState PlayerTwo => playerTwo;
        public LifePointsChangedEvent OnLifePointsChanged => onLifePointsChanged;

        private void Awake()
        {
            if (string.IsNullOrWhiteSpace(playerOne.PlayerId))
            {
                playerOne.Initialize(defaultPlayerOneId, defaultStartingLifePoints);
            }
            else
            {
                playerOne.ResetLifePoints();
            }

            if (string.IsNullOrWhiteSpace(playerTwo.PlayerId))
            {
                playerTwo.Initialize(defaultPlayerTwoId, defaultStartingLifePoints);
            }
            else
            {
                playerTwo.ResetLifePoints();
            }

            NotifyLifePointsChanged(playerOne);
            NotifyLifePointsChanged(playerTwo);
        }

        public void ConfigurePlayers(string playerOneId, string playerTwoId, int startingLifePoints = 8000)
        {
            playerOne.Initialize(playerOneId, startingLifePoints);
            playerTwo.Initialize(playerTwoId, startingLifePoints);
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
