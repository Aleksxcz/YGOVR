using UnityEngine;
using System.Collections.Generic;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;  // Singleton for easy access

    public TextAsset cardsJson; // Assign JSON in the Inspector
    private List<CardData> cardList = new List<CardData>();

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            LoadCards();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadCards()
    {
        if (cardsJson == null)
        {
            Debug.LogError("Cards JSON file not assigned!");
            return;
        }

        // Parse JSON
        CardCollection collection = JsonUtility.FromJson<CardCollection>(cardsJson.text);

        // Store cards in a list
        cardList = new List<CardData>(collection.cards);
    }

    // Public method to get all cards
    public List<CardData> GetAllCards()
    {
        return new List<CardData>(cardList);
    }

    // Public method to search for a card by name
    public CardData GetCardByName(string name)
    {
        return cardList.Find(card => card.CardName.ToLower() == name.ToLower());
    }

    // Public method to get a limited set of cards (pagination)
    public List<CardData> GetCards(int startIndex, int count)
    {
        return cardList.GetRange(startIndex, Mathf.Min(count, cardList.Count - startIndex));
    }
}