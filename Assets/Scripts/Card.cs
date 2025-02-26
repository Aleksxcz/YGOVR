using UnityEngine;
using TMPro; // TextMeshPro for 3D objects

public class Card : MonoBehaviour
{
    public TextMeshPro cardNameText;  // 3D Text for the card name
    public Renderer cardRenderer;     // Card material for image

    public void Setup(CardData data)
    {
        if (cardNameText != null)
            cardNameText.text = data.CardName;  // Set the card name text

        // Load the card texture dynamically
        Texture cardTexture = Resources.Load<Texture>(data.CardImagePath);
        if (cardTexture != null && cardRenderer != null)
            cardRenderer.material.mainTexture = cardTexture; // Set texture
    }
}