using UnityEngine;

public class I_ShoesChestOpen : MonoBehaviour
{
    [SerializeField] private Interaction i;
    public void EquipShoes() 
    {
        _PlayerSave.Instance.shoes = true;
        i.enabled = false;
    }
}
