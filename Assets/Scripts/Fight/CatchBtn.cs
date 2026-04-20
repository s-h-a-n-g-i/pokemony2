using UnityEngine;
using UnityEngine.UI;

public class CatchBtn : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Button>().interactable = !_GlobalPokemon.isItTrainer;
    }



    public void Catching() 
    {
        bool catchCheck = _GlobalPokemon.EnemyPokemon.catchCheck();

        if (catchCheck) 
        {
            
        }
    }
}
