using UnityEngine;

public class ShowAttacksBtn : MonoBehaviour
{
    [SerializeField] private GameObject ShowingCanvas;
    [SerializeField] private GameObject[] otherCanvases;

    public void ButotonPressed() 
    {
        foreach (GameObject obj in otherCanvases) 
        {
            obj.SetActive(false);
        }
        ShowingCanvas.SetActive(true);
    }
}
