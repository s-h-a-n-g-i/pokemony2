using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonCheck : MonoBehaviour
{

    public int mode = 0;
    [HideInInspector] public Pokemon chosenPokemon;

    [SerializeField] private TMP_Text desc;
    [SerializeField] private Image pokemonImage;

    [SerializeField] private TMP_InputField pokename;
    private int pokemonInEq = 0;
    void Start()
    {
        SetupPokemon(0);
    }

    void Update()
    {
        string s = "";
        switch (mode)
        {
            case 0:
                s = chosenPokemon.basicName+" is "+ chosenPokemon.GetPokemonTypes()+". "+" Pokemon is on level "+ chosenPokemon.level + ".";
                break;

            case 1:
                s =
                    "HP:" + chosenPokemon.hp + "/" + chosenPokemon.maxHp + "\n" +
                    "SPEED:" + chosenPokemon.speed + "\n" +
                    "ATK:" + chosenPokemon.atk + "\n" +
                    "SATK:" + chosenPokemon.sAtk + "\n" +
                    "DEF:" + chosenPokemon.def + "\n" +
                    "SDEF:" + chosenPokemon.sDef + "\n" +
                    "XP:" + chosenPokemon.xp
                    ;
                break;

            case 2:
                s =
                    "HP IV:" + chosenPokemon.hpIV + "\n" +
                    "SPEED IV:" + chosenPokemon.speedIV + "\n" +
                    "ATK IV:" + chosenPokemon.atkIV + "\n" +
                    "SATK IV:" + chosenPokemon.sAtkIV + "\n" +
                    "DEF IV:" + chosenPokemon.defIV + "\n" +
                    "SDEF IV:" + chosenPokemon.sDefIV;
                break;

            case 3:
                break;
        }

        desc.text = s;
    }
    public void SaveNameToPokemonButton() 
    {
        if(pokename.text != null && pokename.text != "")
            _PokemonEQ.Instance.EqPokemons[pokemonInEq].nickname = pokename.text;
    }

    public void SetupPokemon(int s) 
    {
        desc.text = "";
        chosenPokemon = _PokemonEQ.Instance.EqPokemons[s];
        pokemonInEq = s;
        if (chosenPokemon.basicName != chosenPokemon.nickname && (chosenPokemon.nickname!=null || chosenPokemon.nickname!=""))
            pokename.text = chosenPokemon.nickname;
        else
            pokename.text = chosenPokemon.basicName;

        switch (chosenPokemon.evoState)
        {
            case 0:
                pokemonImage.sprite = chosenPokemon.image;
                break;
            case 1:
                pokemonImage.sprite = chosenPokemon.E1image;
                break;
            case 2:
                pokemonImage.sprite = chosenPokemon.E2image;
                break;
        }

    }

}
