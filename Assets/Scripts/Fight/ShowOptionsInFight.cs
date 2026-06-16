using UnityEngine;

public class ShowOptionsInFight : MonoBehaviour
{
    [SerializeField] private GameObject ShowingCanvas;
    [SerializeField] private GameObject[] otherCanvases;
    [SerializeField] private bool checkDead = false;

    public void ButotonPressed() 
    {
        foreach (GameObject obj in otherCanvases) 
        {
            obj.SetActive(false);
        }
        ShowingCanvas.SetActive(true);
    }

    private void Update()
    {
        if(_PokemonEQ.Instance.ActivePokemon.hp<=0)
            ButotonPressed();
    }

}
