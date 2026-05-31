using UnityEngine;
using UnityEngine.UI;

public class Pc_ListButton : MonoBehaviour
{
    public Pokemon pokemon;
    

    public void ButtonPressed() 
    {
        if (_PokemonEQ.Instance.EqPokemons[1] == null || _PokemonEQ.Instance.EqPokemons[1].basicName == string.Empty)
        {
            SwapPokemon(1);
        }
        else if (_PokemonEQ.Instance.EqPokemons[2] == null || _PokemonEQ.Instance.EqPokemons[2].basicName == string.Empty)
        {
            SwapPokemon(2);
        }
        else if (_PokemonEQ.Instance.EqPokemons[3] == null || _PokemonEQ.Instance.EqPokemons[3].basicName == string.Empty)
        {
            SwapPokemon(3);
        }
        else if (_PokemonEQ.Instance.EqPokemons[4] == null || _PokemonEQ.Instance.EqPokemons[4].basicName == string.Empty)
        {
            SwapPokemon(4);
        }
    }

    private void SwapPokemon(int SlotId) 
    {
        _PokemonEQ.Instance.EqPokemons[SlotId] = pokemon;
        _PokemonEQ.Instance.AllHavePokemons.Remove(pokemon);
        Destroy(gameObject);
    }
}
