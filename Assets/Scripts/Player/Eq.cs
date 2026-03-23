using System.Collections.Generic;
using UnityEngine;

public class Eq : MonoBehaviour
{
    [SerializeField] private PokemonSO s;
    [SerializeField] private PokemonSO d;
    [SerializeField] private PokemonSO f;
    [SerializeField] private CreatureEq c;
    
    private void Start()
    {
        c.Equipped[0] = new Pokemon();
        c.Equipped[0].CreatePokemon(s, 5);

        c.Equipped[1] = new Pokemon();
        c.Equipped[1].CreatePokemon(d, 5);

        c.Equipped[2] = new Pokemon();
        c.Equipped[2].CreatePokemon(f, 5);


    }

    //public List<ItemsSO> itemsInEq;
}
