using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class I_HealingMonument : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Bobles playerBobles;
    DialogeManager dialoge;
    Interaction interaction;

    private void Start()
    {
        interaction = GetComponent<Interaction>();
    }

    public void HealAllPokemons() 
    {
        playerBobles = GameObject.Find("Player").GetComponent<Bobles>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        dialoge = GameObject.Find("GameManager").GetComponent<DialogeManager>();
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.healingSound,transform.position);
        foreach (Pokemon s in _PokemonEQ.Instance.EqPokemons)
        {
            s.effects = Effects.None;
            s.atkX = 0;
            s.defX = 0;
            s.sDefX = 0;
            s.sAtkX = 0;
            s.speedX = 0;
            s.accuracyX = 0;
            s.hp = s.maxHp;
            s.resetAttacksPPs();
        }
        StartCoroutine(HealDialoge());
    }


    private IEnumerator HealDialoge()
    {
        playerMovement.StopPlayer();
        yield return StartCoroutine(playerBobles.loveBobel());
        yield return StartCoroutine(dialoge.DialogeShow("All Creatures Has been healed!"));
        playerMovement.StartPlayer();
        interaction.canInteract = true;
    }
}
