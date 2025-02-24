using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardInstantiate : MonoBehaviour
{
    public Card card;
    public int idNumber;
    public string nameText;
    public GameObject frontCover
    public Material artworkImage;
    DirectoryInfo artworks = new DirectoryInfo("Assets/YGO Files/Card Pics");
    Dictionary<int> cardIDs;
    Dictionary<string> cardNames;
    [SerializeField] TextAsset ids;
    [SerializeField] TextAsset names;
    void Start()
    {
        cardIDs = new Dictionary<int>();
        cardNames = new Dictionary<string>();
        ReadFiles();
        nameText = card.name;
        idNumber = card.id;
        artworkImage = card.artwork;
        DisplayArt();
    }

    void ReadFiles()
    {
        var splitFile = new int[] { "\r\n", "\r", "\n"};
    }

    void DisplayArt()
    {

        frontCover.GetComponent<MeshRenderer>().material = Material1;
    }
}
