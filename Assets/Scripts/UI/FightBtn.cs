using TMPro;
using UnityEngine;

public class FightBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text AttackNameText;
    [SerializeField] private CreatureEq pokemons;
    [SerializeField] private int attackCounter;
    private Attacks Attacks;
    private Pokemon pokemon;



    void Update()
    {
        
    }


    public void UpdateAttack()
    {
        pokemon = pokemons.ActivePokemon;
    }
}
