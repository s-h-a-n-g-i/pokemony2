using UnityEngine;
using UnityEngine.UI;

public class PokemonButtonChosen : MonoBehaviour
{
    public bool picked = false;

    private void Update()
    {
        GetComponent<Button>().interactable = picked;
    }
}
