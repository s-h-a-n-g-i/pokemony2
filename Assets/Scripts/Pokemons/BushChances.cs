using UnityEngine;

[System.Serializable]
public class BushChances
{
    public PokemonSO pokemon;
    [Range(0, 100)] public float chances;
}
