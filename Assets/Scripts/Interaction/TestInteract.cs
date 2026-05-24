using UnityEngine;

public class TestInteract : MonoBehaviour
{
    Interaction interaction;

    void Start()
    {
        interaction = GetComponent<Interaction>();
    }

    public void Sperma(string s)
    {
        Debug.Log(s);
        interaction.canInteract = true;
    }
    public void AddXp(int xp)
    {
        _PokemonEQ.Instance.EqPokemons[0].xp += xp;
        Debug.Log("Added XP:" + xp);
        interaction.canInteract = true;
    }
    public void AddXpNdPokemon(int xp)
    {
        _PokemonEQ.Instance.EqPokemons[1].xp += xp;
        Debug.Log("Added XP:" + xp);
        interaction.canInteract = true;
    }

    public void RemoveHp(int hp) 
    {
        _PokemonEQ.Instance.EqPokemons[0].hp -= hp;
        Debug.Log("Removed HP:" + hp);
        interaction.canInteract = true;
    }
}
