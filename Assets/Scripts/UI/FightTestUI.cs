using TMPro;
using UnityEngine;

public class FightTestUI : MonoBehaviour
{

    [SerializeField] private FightingPokemons f;
    
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();

        text.text =f.pokemonToBattle.basicName + " " + f.pokemonToBattle.atk + " : " + f.pokemonToBattle.level;
    }

    void Update()
    {
        
    }
}
