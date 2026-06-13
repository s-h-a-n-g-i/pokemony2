using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossCutscene : MonoBehaviour
{

    [SerializeField] private DialogeLine[] dialogeLines;
    [SerializeField] private PokemonSO[] pokemonToFight;
    private Pokemon[] pokemon;
    [SerializeField] private int level;
    public bool StartFight = false;
    public int StepSound = 0;

    private int stepcounter = 0;
    private bool started = false;

    private DialogeManager dialoge;

    private void Start()
    {
        pokemon = new Pokemon[pokemonToFight.Length];
        dialoge = GameObject.Find("GameManager").GetComponent<DialogeManager>();
        for (int i = 0; i < pokemon.Length; i++)
        {
            pokemon[i] = new Pokemon(pokemonToFight[i], level);
        }
    }

    private void Update()
    {
        if (stepcounter != StepSound) 
        {
            stepcounter = StepSound;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.walkingSound,transform.position);
        }

        if (StartFight && !started) 
        {
            started = true;
            StartCoroutine(StartFightCutscene());
        }
    }

    IEnumerator StartFightCutscene() 
    {
        _NPCManager.Instance.canescape = false;
        _NPCManager.Instance.TrainerName = "Devil";
        _NPCManager.Instance.isItTrainer = true;
        _NPCManager.Instance.TrainerPokemons = pokemon;

        _PlayerSave.Instance.placed = false;
        
        foreach (DialogeLine dialogeLine in dialogeLines)
            yield return StartCoroutine(dialoge.DialogeShow(dialogeLine.whoSayes + ": " + dialogeLine.whatSayes));
        SceneManager.LoadScene("FightNew");
    }


}
