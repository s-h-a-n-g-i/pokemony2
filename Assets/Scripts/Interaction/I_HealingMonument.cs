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
        StartCoroutine(HealDialoge());
        foreach (Pokemon s in _PokemonEQ.Instance.EqPokemons)
        {
            if(s==null) return;
            if (s.basicName == string.Empty) return;
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
    }


    private IEnumerator HealDialoge()
    {
        playerMovement.StopPlayer();
        yield return StartCoroutine(playerBobles.loveBobel());
        yield return StartCoroutine(dialoge.DialogeShow("This fountain warms you up and restores your health.\r\n"));
        playerMovement.StartPlayer();
        interaction.canInteract = true;
    }
}
