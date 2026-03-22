using UnityEngine;

public enum itemsTypes 
{
    ball,
    potion
}

[CreateAssetMenu(menuName = "ScriptableObjects/Items")]
public class ItemsSO : ScriptableObject
{
    public string itemName;
    public itemsTypes type;

}
