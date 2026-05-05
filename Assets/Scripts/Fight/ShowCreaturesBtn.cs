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
        if (_PokemonEQ.Instance.EqPokemons[pokemonCounter] == null)
        {
            PokemonNameText.text = "n ma nc";
            gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        else 
        {
            gameObject.GetComponent<Button>().interactable = true;
        }

        if (_PokemonEQ.Instance.EqPokemons[pokemonCounter] == _PokemonEQ.Instance.ActivePokemon || _PokemonEQ.Instance.EqPokemons[pokemonCounter].hp <=0)
            gameObject.GetComponent<Button>().interactable = false;

        PokemonNameText.text = _PokemonEQ.Instance.EqPokemons[pokemonCounter].PokemonNameOut() + "HP " + _PokemonEQ.Instance.EqPokemons[pokemonCounter].maxHp + "/" + _PokemonEQ.Instance.EqPokemons[pokemonCounter].hp;
        
        
    }

    public void ShowCreaturePressed() 
    {

        fightSystemManager.chosenPokemonPlayer = pokemonCounter;
        fightSystemManager.setUpMyPokemon();

        if (_NPCManager.Instance.isItTrainer) 
        {
            trainerManager.ChangePokemon();
        }


        Debug.Log(_PokemonEQ.Instance.EqPokemons[pokemonCounter].PokemonNameOut());
    }
}
