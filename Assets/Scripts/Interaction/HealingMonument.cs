using UnityEngine;

public class HealingMonument : MonoBehaviour
{
    public void HealAllPokemons() 
    {
        foreach (Pokemon s in _PokemonEQ.Instance.EqPokemons)
            s.hp = s.maxHp;
        
    }
}
