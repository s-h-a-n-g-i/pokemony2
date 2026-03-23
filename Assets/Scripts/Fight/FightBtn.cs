using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text AttackNameText;
    [SerializeField] private CreatureEq pokemons;
    [SerializeField] private PokemonInFightSO fightingPokemons;
    [SerializeField] private int attackCounter;
    [SerializeField] private TrainerManager trainerManager;
    private Attack Attacks;
    private Pokemon pokemon;


    void Update()
    {
        pokemon = pokemons.ActivePokemon;
        UpdateAttack(pokemon.AttacksActive[attackCounter]);
    }


    public void UpdateAttack(Attack attack)
    {
        AttackNameText.text = attack.attackName + " (" + attack.maxPp + "/" +attack.pp + ")";
        
    }

    public void AttackUse()
    {
        StartCoroutine(trainerManager.Attack(pokemon.AttacksActive[attackCounter].howFastAttackIs(pokemon), pokemon.AttacksActive[attackCounter], pokemon));
    }

}
