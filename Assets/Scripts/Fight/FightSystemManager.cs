using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightSystemManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PokemonInFightSO FightSO;
    [SerializeField] private CreatureEq CreatureEqSO;
    [SerializeField] private Eq EqSO;


    [Header("My Pokemon")]
    [SerializeField] private Image myPokemonImage;
    [SerializeField] private TMP_Text myPokemonName;


    [Header("Managers")]
    [SerializeField] private SingleFightManager singleFight;
    [SerializeField] private TrainerManager trainerFight;




    public int chosenPokemonPlayer = 0;

    private void Awake()
    {
        if(FightSO.isThisTrainer)
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
        if (CreatureEqSO.ActivePokemon != CreatureEqSO.Equipped[chosenPokemonPlayer]) 
        {
            CreatureEqSO.ActivePokemon = CreatureEqSO.Equipped[chosenPokemonPlayer];
        }

        myPokemonImage.sprite = CreatureEqSO.ActivePokemon.image;
        myPokemonName.text = CreatureEqSO.ActivePokemon.PokemonNameOut();
    }

    public void Attacking(int playerAttackCounter) 
    {
        if (FightSO.isThisTrainer)
            trainerFight.Attack(playerAttackCounter);
        //else
        //    singleFight.Attack();
    
    }


}
