using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text AttackNameText;
    [SerializeField] private CreatureEq pokemons;
    [SerializeField] private PokemonInFightSO fightingPokemons;
    [SerializeField] private int attackCounter;
    [SerializeField] private FightSystemManager fightManager;
    


    void Update()
    {
        UpdateAttack(pokemons.ActivePokemon.AttacksActive[attackCounter]);
    }


    public void UpdateAttack(Attack attack)
    {
        AttackNameText.text = attack.attackName + " (" + attack.maxPp + "/" +attack.pp + ")";
        
    }

    public void AttackUse()
    {
        fightManager.Attacking(attackCounter);
    }

}
