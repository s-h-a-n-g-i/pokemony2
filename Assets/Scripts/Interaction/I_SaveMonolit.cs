using UnityEngine;

public class I_SaveMonolit : MonoBehaviour
{

    DialogeManager dialoge;
    Interaction interaction;


    void Start()
    {
        interaction = GetComponent<Interaction>();
        dialoge = GameObject.Find("GameManager").GetComponent<DialogeManager>();
    }

    public void InteractionUsedFunction()
    {
        StartCoroutine(dialoge.SaveGameMenu());
        interaction.canInteract = true;
    }

}