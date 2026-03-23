using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ShowCreaturesBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text PokemonNameText;
    [SerializeField] private CreatureEq pokemons;
    [SerializeField] private PokemonInFightSO fightingPokemons;
    [SerializeField] private FightSystemManager fightSystemManager;
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
        fightSystemManager.chosenPokemonPlayer = pokemonCounter;
        fightSystemManager.setUpMyPokemon();
        Debug.Log(pokemons.Equipped[pokemonCounter].PokemonNameOut());
    }
}
