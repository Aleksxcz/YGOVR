using UnityEngine;

namespace YGOVR.Gameplay
{
    [CreateAssetMenu(fileName = "CardData", menuName = "YGOVR/Cards/Card Data")]
    public class CardData : ScriptableObject
    {
        [SerializeField] private string cardId;
        [SerializeField] private string cardName;
        [SerializeField] private string description;
        [SerializeField] private Sprite artwork;
        [SerializeField] private int attack;
        [SerializeField] private int defense;

        public string CardId => cardId;
        public string CardName => cardName;
        public string Description => description;
        public Sprite Artwork => artwork;
        public int Attack => attack;
        public int Defense => defense;
    }
}
