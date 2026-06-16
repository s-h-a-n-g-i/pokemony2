using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarteFinish : MonoBehaviour
{
    [SerializeField] private bool startDialogeToFinish = false;
    private bool checkOnce = false;
    [SerializeField] private DialogeLine[] dialogeLines;
    DialogeManager dialogeManager;

    [SerializeField] private Animator animator;
    private void Start()
    {
        dialogeManager = GameObject.Find("GameManager").GetComponent<DialogeManager>();
    }


    void Update()
    {
        if (startDialogeToFinish && !checkOnce) 
        {
            checkOnce = true;
            StartCoroutine(dialogeAndFinish());
        }
    }


    IEnumerator dialogeAndFinish() 
    {
        foreach (DialogeLine d in dialogeLines)
            yield return StartCoroutine(dialogeManager.DialogeShow(d.whoSayes+": "+d.whatSayes));

        animator.SetTrigger("Finish");

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
