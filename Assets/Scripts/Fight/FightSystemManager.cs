using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightSystemManager : MonoBehaviour
{
  
    [Header("My Pokemon")]
    [SerializeField] private Image myPokemonImage;
    [SerializeField] private TMP_Text myPokemonName;


    [Header("Managers")]
    [SerializeField] private SingleFightManager singleFight;
    [SerializeField] private TrainerManager trainerFight;




    public int chosenPokemonPlayer = 0;

    private void Awake()
    {
        if(_GlobalPokemon.isItTrainer)
            singleFight.enabled = false;
        else
            trainerFight.enabled = false;
    }

    void Start()
    {
        setUpMyPokemon();
        
    }

    void Update()
    {
        
    }


    public void setUpMyPokemon()
    {
        if (_GlobalPokemon.ActivePokemon != _GlobalPokemon.EqPokemons[chosenPokemonPlayer]) 
        {
            _GlobalPokemon.ActivePokemon = _GlobalPokemon.EqPokemons[chosenPokemonPlayer];
        }

        myPokemonImage.sprite = _GlobalPokemon.ActivePokemon.image;
        myPokemonName.text = _GlobalPokemon.ActivePokemon.PokemonNameOut();
    }

    public void Attacking(int playerAttackCounter) 
    {
        if (_GlobalPokemon.isItTrainer)
            trainerFight.Attack(playerAttackCounter);
        //else
        //    singleFight.Attack();
    
    }


}
