using UnityEngine;

public class TestInteract : MonoBehaviour
{
    public void Sperma(string s)
    {
        Debug.Log(s);
    }
    public void AddXp(int xp)
    {
        _PokemonEQ.Instance.EqPokemons[0].xp+=xp;
        Debug.Log("Added XP:" + xp);
    }
}
