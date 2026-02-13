using UnityEngine;

public enum itemsTypes 
{
    ball,
    potion
}

[CreateAssetMenu(menuName = "ScriptableObjects/Items")]
public class Items : ScriptableObject
{
    public string itemName;
    public itemsTypes type;

}
