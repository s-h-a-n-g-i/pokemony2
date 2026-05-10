using UnityEngine;
using UnityEngine.UI;

public class CatchBtn : MonoBehaviour
{
    [SerializeField] SingleFightManager singleManager;
    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Button>().interactable = !_NPCManager.Instance.isItTrainer;
    }



    public void Catching() 
    {
        
        bool catchCheck = _FightManager.Instance.EnemyPokemon.catchCheck();

        if (catchCheck)
        {
            singleManager.CatchPokemonTrue();
        }
        else
        {
            singleManager.CatchPokemonFalse();
        }
    }
}
