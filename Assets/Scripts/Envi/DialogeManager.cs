using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogeManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogeObject;
    [SerializeField] private TMP_Text dialogeText;
    public bool dialogeFinished = true;
    public bool saveChosen = true;

    [SerializeField] private GameObject SaveButtons;
    float speedwagon = 0.05f;
    //ide srac
    void Awake()
    {
        DontDestroyOnLoad(dialogeObject);
    }

    private void Update()
    {
        dialogeObject.SetActive(!dialogeFinished);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            speedwagon = 0;
        }
    }

    public IEnumerator DialogeShow(string textToEnter)
    {
        textToEnter += " (Z)";
        speedwagon = 0.05f;
        dialogeText.text = "";
        dialogeFinished = false;
        textToEnter = textToEnter.Replace("player", _PlayerSave.Instance.playerName);
        string oryginal = textToEnter;
        textToEnter = textToEnter.Replace("<b>", "|");
        textToEnter = textToEnter.Replace("</b>", ";");
        textToEnter = textToEnter.Replace("<i>", "/");
        textToEnter = textToEnter.Replace("</i>", "=");
        //Debug.Log(textToEnter);

        for (int i = 0; i < textToEnter.Length; i++)
        {
            switch (textToEnter[i])
            {
                case '|':
                    dialogeText.text += "<b>";
                    break;
                case ';':
                    dialogeText.text += "</b>";
                    break;
                case '/':
                    dialogeText.text += "<i>";
                    break;
                case '=':
                    dialogeText.text += "</i>";
                    break;
                default:
                    dialogeText.text += textToEnter[i];
                    break;
            }

            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.dialogeSound, transform.position);

            yield return new WaitForSeconds(speedwagon);
            if (speedwagon == 0)
            {
                dialogeText.text = oryginal;
                break;
            }
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space));
        dialogeFinished = true;
    }

    public IEnumerator SaveGameMenu()
    {
        dialogeFinished = false;
        SaveButtons.SetActive(true);
        saveChosen = false;
        dialogeText.text = "Save game on slot:";
        yield return new WaitUntil(()=>saveChosen);
        SaveButtons.SetActive(false);
        dialogeFinished = true;
    }

    private void OnDestroy()
    {
        Destroy(dialogeObject);
    }

}
