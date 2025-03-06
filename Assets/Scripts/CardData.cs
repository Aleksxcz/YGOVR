using System;

[Serializable]
public class CardData
{
    public string CardID;
    public string CardName;
    public string CardImagePath;
    public string CardImagePathCropped;
    public string type;
    public string frameType;
    public string desc;
    public int atk;
    public int def;
    public int level;
    public string race;
    public string attribute;
    
    
}

[Serializable]
public class CardCollection
{
    public CardData[] cards;
}