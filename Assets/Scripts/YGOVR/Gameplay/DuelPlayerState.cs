using System;
using UnityEngine;

namespace YGOVR.Gameplay
{
    [Serializable]
    public class DuelPlayerState
    {
        [SerializeField] private string playerId;
        [SerializeField] private int startingLifePoints = 8000;
        [SerializeField] private int currentLifePoints = 8000;

        public string PlayerId => playerId;
        public int StartingLifePoints => startingLifePoints;
        public int CurrentLifePoints => currentLifePoints;

        public void ResetLifePoints()
        {
            currentLifePoints = Mathf.Max(0, startingLifePoints);
        }

        public int ApplyDamage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Damage amount cannot be negative.");
            }

            currentLifePoints = Mathf.Max(0, currentLifePoints - amount);
            return currentLifePoints;
        }

        public int RestoreLifePoints(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Restore amount cannot be negative.");
            }

            currentLifePoints = Mathf.Max(0, currentLifePoints + amount);
            return currentLifePoints;
        }
    }
}
