using Unity.VisualScripting;
using UnityEngine;

public class I_Pc : MonoBehaviour
{
    Interaction interaction;

    [SerializeField] GameObject PcCanvas;
    void Start()
    {
        interaction = GetComponent<Interaction>();
    }

    public void InteractionUsedFunction()
    {
        PcCanvas.SetActive(true);
        interaction.canInteract = true;
    }
}
