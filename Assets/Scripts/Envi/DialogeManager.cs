using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Rendering.MaterialUpgrader;

public class DialogeManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogeObject;
    [SerializeField] private TMP_Text dialogeText;
    public bool dialogeFinished = true;


    float speedwagon = 0.05f;
    //ide srac
    void Awake()
    {
        //DialogeManager[] myItems = FindObjectsByType<DialogeManager>(FindObjectsSortMode.None);

        //foreach (DialogeManager item in myItems)
        //{
        //    if (item.gameObject != this)
        //    {
        //        Destroy(dialogeObject);
        //        Destroy(this);
        //    }
        //}
        //DontDestroyOnLoad(this);
        DontDestroyOnLoad(dialogeObject);
    }

    private void Update()
    {
        dialogeObject.SetActive(!dialogeFinished);
    }


    public IEnumerator DialogeShow(string textToEnter)
    {
        speedwagon = 0.05f;
        dialogeText.text = "";
        dialogeFinished = false;
        string oryginal = textToEnter;
        textToEnter = textToEnter.Replace("<b>", "|");
        textToEnter = textToEnter.Replace("</b>", ";");
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
                default:
                    dialogeText.text += textToEnter[i];
                    break;
            }

            yield return new WaitForSeconds(speedwagon);
            if (speedwagon == 0)
            {
                dialogeText.text = oryginal;
                break;
            }
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        dialogeFinished = true;
    }



}
