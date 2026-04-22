using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonCheckButton : MonoBehaviour
{
    [SerializeField] private TMP_Text pokeName;
    [SerializeField] private PokemonCheck pokeCheck;
    [SerializeField] private int pokemonChosen;

    private void Update()
    {
        if (_GlobalPokemon.EqPokemons[pokemonChosen] == null)
        {
            GetComponent<Button>().interactable = false;
            pokeName.text = "No Pokemon Avible";
        }
        else
        {
            GetComponent<Button>().interactable = true;
            pokeName.text = _GlobalPokemon.EqPokemons[pokemonChosen].PokemonNameOut();
        }
    }

    public void ButtonCheckPressed() 
    {
        pokeCheck.SetupPokemon(pokemonChosen);
        
    }
}
