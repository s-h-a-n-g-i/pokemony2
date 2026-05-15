using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealingMonument : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Bobles playerBobles;
    DialogeManager dialoge;
    public void HealAllPokemons() 
    {
        playerBobles = GameObject.Find("Player").GetComponent<Bobles>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        dialoge = GameObject.Find("GameManager").GetComponent<DialogeManager>();

        foreach (Pokemon s in _PokemonEQ.Instance.EqPokemons)
            s.hp = s.maxHp;
        StartCoroutine(HealDialoge());
    }


    private IEnumerator HealDialoge()
    {
        playerMovement.StopPlayer();
        playerBobles.loveBobel();
            yield return StartCoroutine(dialoge.DialogeShow("All Creatures Has been healed!"));
        playerMovement.StartPlayer();
    }
}
