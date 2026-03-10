using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ShowCreaturesBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text PokemonNameText;
    [SerializeField] private CreatureEq pokemons;
    [SerializeField] private FightingPokemons fightingPokemons;

    [SerializeField] private int pokemonCounter;

    void Update()
    {
        if (pokemons.Equipped[pokemonCounter] == null)
        {   
            gameObject.SetActive(false);
            return; 
        }

        PokemonNameText.text = pokemons.Equipped[pokemonCounter].PokemonNameOut() + "HP " + pokemons.Equipped[pokemonCounter].hp + "/" + pokemons.Equipped[pokemonCounter].hp;
        
        
    }

    public void ShowCreaturePressed() 
    {
        Debug.Log(pokemons.Equipped[pokemonCounter].PokemonNameOut());
    }
}
