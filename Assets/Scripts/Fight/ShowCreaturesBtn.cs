using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCreaturesBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text PokemonNameText;
    [SerializeField] private FightSystemManager fightSystemManager;
    [SerializeField] private TrainerManager trainerManager;
    [SerializeField] private int pokemonCounter;

    void Update()
    {
        if (_GlobalPokemon.EqPokemons[pokemonCounter] == null)
        {
            PokemonNameText.text = "n ma nc";
            gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        else 
        {
            gameObject.GetComponent<Button>().interactable = true;
        }

        if (_GlobalPokemon.EqPokemons[pokemonCounter] == _GlobalPokemon.ActivePokemon || _GlobalPokemon.EqPokemons[pokemonCounter].hp <=0)
            gameObject.GetComponent<Button>().interactable = false;

        PokemonNameText.text = _GlobalPokemon.EqPokemons[pokemonCounter].PokemonNameOut() + "HP " + _GlobalPokemon.EqPokemons[pokemonCounter].maxHp + "/" + _GlobalPokemon.EqPokemons[pokemonCounter].hp;
        
        
    }

    public void ShowCreaturePressed() 
    {

        fightSystemManager.chosenPokemonPlayer = pokemonCounter;
        fightSystemManager.setUpMyPokemon();

        if (_GlobalPokemon.isItTrainer) 
        {
            trainerManager.ChangePokemon();
        }


        Debug.Log(_GlobalPokemon.EqPokemons[pokemonCounter].PokemonNameOut());
    }
}
