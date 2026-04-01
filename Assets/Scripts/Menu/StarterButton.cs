using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterButton : MonoBehaviour
{
    [SerializeField] PokemonSO Starter;

    public void ButtonClicked() 
    {
        _GlobalPokemon.EqPokemons[0] = new Pokemon(Starter, 3);
        SceneManager.LoadScene("SampleScene");
    }
}
