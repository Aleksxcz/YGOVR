using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    // public GameObject cardPrefab; // Assign in Inspector
    // public Transform cardContainer; // UI Panel or Scene Object to hold cards
    // public int cardsPerPage = 20; // How many cards to show at once

    // private int currentPage = 0;
    // private List<CardData> allCards;

    // void Start()
    // {
    //     allCards = CardDatabase.Instance.GetAllCards();
    //     ShowPage(0);
    // }

    // void ShowPage(int page)
    // {
    //     // Clear existing cards
    //     foreach (Transform child in cardContainer)
    //     {
    //         Destroy(child.gameObject);
    //     }

    //     // Get cards for this page
    //     List<CardData> pageCards = CardDatabase.Instance.GetCards(page * cardsPerPage, cardsPerPage);

    //     // Instantiate card prefabs
    //     foreach (var cardData in pageCards)
    //     {
    //         GameObject newCard = Instantiate(cardPrefab, cardContainer);
    //         newCard.GetComponent<Card>().Setup(cardData);
    //     }
    // }

    // // Called when "Next Page" button is pressed
    // public void NextPage()
    // {
    //     if ((currentPage + 1) * cardsPerPage < allCards.Count)
    //     {
    //         currentPage++;
    //         ShowPage(currentPage);
    //     }
    // }

    // // Called when "Previous Page" button is pressed
    // public void PreviousPage()
    // {
    //     if (currentPage > 0)
    //     {
    //         currentPage--;
    //         ShowPage(currentPage);
    //     }
    // }
}