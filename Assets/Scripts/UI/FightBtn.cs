using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text AttackNameText;
    [SerializeField] private CreatureEq pokemons;
    [SerializeField] private FightingPokemons fightingPokemons;
    [SerializeField] private int attackCounter;
    [SerializeField] private FightManager fightManager;
    private Attack Attacks;
    private Pokemon pokemon;
    private string sperma = "poldek je sperme";


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
        //fightManager.Attack(pokemon.AttacksActive[attackCounter]);
    }

}
