using System;

[Serializable]
public class CardData
{
    public string CardID;
    public string CardName;
    public string CardImagePath;
}

[Serializable]
public class CardCollection
{
    public CardData[] cards;
}