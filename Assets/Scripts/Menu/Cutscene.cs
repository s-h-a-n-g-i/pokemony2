using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private DialogeLine[] dialogeLines;
    [SerializeField] private GameObject wholeUI;
    private DialogeManager dialogeManager;
    void Start()
    {
        dialogeManager = GameObject.Find("GameManager").GetComponent<DialogeManager>();
        StartCoroutine(startCutscene());
    }
    private IEnumerator startCutscene() 
    {
        foreach (DialogeLine d in dialogeLines) 
        {
            yield return StartCoroutine(dialogeManager.DialogeShow(d.whoSayes+": "+d.whatSayes));
        }

        wholeUI.SetActive(false);
    }
}
