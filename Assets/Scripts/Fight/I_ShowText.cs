using System.Collections;
using UnityEngine;

public class I_ShowText : MonoBehaviour
{
    private DialogeManager dialoge;
    private Interaction interaction;
    private PlayerMovement playerMovement;
    private Bobles playerBobles;

    [TextArea(10, 30)]
    [SerializeField] private string[] textToShow;

    void Start()
    {
        interaction = GetComponent<Interaction>();
        dialoge = GameObject.Find("GameManager").GetComponent<DialogeManager>();
    }

    public void ShowText()
    {
        playerBobles = GameObject.Find("Player").GetComponent<Bobles>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        StartCoroutine(StartText());
    }


    private IEnumerator StartText()
    {
        playerBobles.questBobelShow();
        playerMovement.StopPlayer();
        playerMovement.StopMoving();
        foreach (string s in textToShow)
            yield return StartCoroutine(dialoge.DialogeShow(s));
        playerMovement.StartPlayer();
        playerBobles.clearBobels();
        interaction.canInteract = true;
    }
}
