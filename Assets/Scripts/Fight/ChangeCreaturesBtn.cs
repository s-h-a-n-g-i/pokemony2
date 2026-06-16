using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ChangeCreaturesBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text PokemonNameText;
    [SerializeField] private FightSystemManager fightSystemManager;
    [SerializeField] private TrainerManager trainerManager;
    [SerializeField] private SingleFightManager singleManager;
    [SerializeField] private GameObject TextShow;
    [SerializeField] private GameObject CreaturesChose;
    [SerializeField] private int pokemonCounter;

    void Update()
    {
        string s = "";

        if (_PokemonEQ.Instance.EqPokemons[pokemonCounter] == null || _PokemonEQ.Instance.EqPokemons[pokemonCounter].basicName == string.Empty)
        {
            s = "EMPTY";
            gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        else 
        {
            s = _PokemonEQ.Instance.EqPokemons[pokemonCounter].PokemonNameOut() + "HP " + _PokemonEQ.Instance.EqPokemons[pokemonCounter].maxHp + "/" + _PokemonEQ.Instance.EqPokemons[pokemonCounter].hp;
            gameObject.GetComponent<Button>().interactable = true;
        }

        if (_PokemonEQ.Instance.EqPokemons[pokemonCounter] == _PokemonEQ.Instance.ActivePokemon || _PokemonEQ.Instance.EqPokemons[pokemonCounter].hp <= 0)
        { 
            gameObject.GetComponent<Button>().interactable = false;
            s = _PokemonEQ.Instance.EqPokemons[pokemonCounter].PokemonNameOut() + "HP " + _PokemonEQ.Instance.EqPokemons[pokemonCounter].maxHp + "/" + _PokemonEQ.Instance.EqPokemons[pokemonCounter].hp;
        }

        PokemonNameText.text = s;
        
        
    }

    public void ShowCreaturePressed()
    {

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.swap, transform.position);
        TextShow.SetActive(true);
        CreaturesChose.SetActive(false);
        _PokemonEQ.Instance.pokemonUsedInFight.Add(pokemonCounter);
        fightSystemManager.chosenPokemonPlayer = pokemonCounter;
        fightSystemManager.setUpMyPokemon();
        if (_NPCManager.Instance.isItTrainer)
            trainerManager.ChangePokemon();
        else
            singleManager.ChangePokemon();

            //Debug.Log(_PokemonEQ.Instance.EqPokemons[pokemonCounter].PokemonNameOut());
    }
}
