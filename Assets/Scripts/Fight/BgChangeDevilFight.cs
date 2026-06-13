using UnityEngine;
using UnityEngine.UI;

public class BgChangeDevilFight : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite netherBg;


    void Start()
    {
        if (_NPCManager.Instance.TrainerName == "Devil") 
        {
            img.sprite = netherBg;
        }
    }
}
