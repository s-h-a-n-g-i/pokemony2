using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ShowCreaturesBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text PokemonNameText;
    [SerializeField] private FightSystemManager fightSystemManager;
    [SerializeField] private int pokemonCounter;

    void Update()
    {
        if (_GlobalPokemon.EqPokemons[pokemonCounter] == null)
        {   
            gameObject.SetActive(false);
            return; 
        }

        PokemonNameText.text = _GlobalPokemon.EqPokemons[pokemonCounter].PokemonNameOut() + "HP " + _GlobalPokemon.EqPokemons[pokemonCounter].maxHp + "/" + _GlobalPokemon.EqPokemons[pokemonCounter].hp;
        
        
    }

    public void ShowCreaturePressed() 
    {
        fightSystemManager.chosenPokemonPlayer = pokemonCounter;
        fightSystemManager.setUpMyPokemon();
        Debug.Log(_GlobalPokemon.EqPokemons[pokemonCounter].PokemonNameOut());
    }
}
