using UnityEngine;

public class PokemonChangeMode : MonoBehaviour
{
    [SerializeField] private int mode;
    [SerializeField] PokemonCheck pokeCheck;

    public void ButtonChangeModePressed() 
    {
        pokeCheck.mode = mode;
    }
}
