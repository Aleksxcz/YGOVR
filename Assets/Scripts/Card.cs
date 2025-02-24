using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    public new string name;
    public int id;
    public Material artwork;
}
