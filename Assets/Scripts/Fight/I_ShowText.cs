using System.Collections;
using UnityEngine;

public class I_ShowText : MonoBehaviour
{
    private DialogeManager dialoge;
    private Interaction interaction;
    private PlayerMovement playerMovement;
    private Bobles playerBobles;

    [TextArea(10, 30)]
    [SerializeField] private string textToShow;

    void Start()
    {
        interaction = GetComponent<Interaction>();
        dialoge = GameObject.Find("GameManager").GetComponent<DialogeManager>();
    }

    public void ShowText()
    {
        playerBobles = GameObject.Find("Player").GetComponent<Bobles>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        dialoge.StartCoroutine(dialoge.DialogeShow(textToShow));
        StartCoroutine(StartText());
    }


    private IEnumerator StartText()
    {
        playerMovement.StopPlayer();
        yield return StartCoroutine(playerBobles.dotsBobel());
        yield return StartCoroutine(dialoge.DialogeShow(textToShow));
        playerMovement.StartPlayer();
        interaction.canInteract = true;
    }
}
