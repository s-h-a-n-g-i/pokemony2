using UnityEngine;

public class TestInteract : MonoBehaviour
{
    public void Sperma(string s)
    {
        Debug.Log(s);
    }
    public void AddXp(int xp)
    {
        _PokemonEQ.Instance.EqPokemons[0].xp += xp;
        Debug.Log("Added XP:" + xp);
    }
    public void AddXpNdPokemon(int xp)
    {
        _PokemonEQ.Instance.EqPokemons[1].xp += xp;
        Debug.Log("Added XP:" + xp);
    }

    public void RemoveHp(int hp) 
    {
        _PokemonEQ.Instance.EqPokemons[0].hp -= hp;
        Debug.Log("Removed HP:" + hp);
    }
}
