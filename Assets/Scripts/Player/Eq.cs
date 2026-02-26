using System.Collections.Generic;
using UnityEngine;

public class Eq : MonoBehaviour
{
    [SerializeField] private Creatures s;
    [SerializeField] private CreatureEq c;
    
    private void Start()
    {
        c.Equipped[0] = new Pokemon();
        c.Equipped[0].CreatePokemon(s,5);
    }

    public List<Items> itemsInEq;
}
