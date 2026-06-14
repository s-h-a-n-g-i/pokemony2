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
        if (_PokemonEQ.Instance.EqPokemons[pokemonChosen] != null)
        {
            if (_PokemonEQ.Instance.EqPokemons[pokemonChosen].basicName == string.Empty)
            {
                GetComponent<Button>().interactable = false;
                pokeName.text = "No Pokemon Avible";
            }
            else
            {
                GetComponent<Button>().interactable = true;
                pokeName.text = _PokemonEQ.Instance.EqPokemons[pokemonChosen].PokemonNameOut();
            }
        }
        else
        {
            GetComponent<Button>().interactable = false;
            pokeName.text = "No Pokemon Avible";
        }
    }

    public void ButtonCheckPressed() 
    {
        pokeCheck.ResetDesc();
        pokeCheck.SetupPokemon(pokemonChosen);
    }
}
