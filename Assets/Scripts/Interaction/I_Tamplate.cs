using UnityEngine;

public class I_Tamplate : MonoBehaviour
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
        interaction.canInteract = true;
    }

}
