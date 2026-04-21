using UnityEngine;
using UnityEngine.UI;

public class DeadPokemonOptions : MonoBehaviour
{

    [SerializeField] private FightSystemManager fightSystemManager;

    [SerializeField] private Image imageObject;
    [SerializeField] private Sprite image;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button chosePokemon;
    //private bool checkOnce = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (_GlobalPokemon.ActivePokemon.hp == 0)
        {
            attackBtn.interactable = false;
            chosePokemon.onClick.Invoke();
        }
        else if(_GlobalPokemon.ActivePokemon.hp != 0) 
        {
            //checkOnce = true;
            attackBtn.interactable = true;
        }
    }
}
