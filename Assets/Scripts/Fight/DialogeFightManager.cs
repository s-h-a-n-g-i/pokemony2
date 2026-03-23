using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Rendering.MaterialUpgrader;

public class DialogeFightManager : MonoBehaviour
{

    [Header("DialogeWindow")]
    [SerializeField] private GameObject dialogeWindow;
    [SerializeField] private TMP_Text dialogeText;


    //[HideInInspector] public List<string> queueText = new List<string>();
    [HideInInspector] public bool playerFirstAttack = true;

    [HideInInspector] public int enemyDamage;
    [HideInInspector] public int playerDamage;

    [HideInInspector] public bool dialogeFinished = true;
    [HideInInspector] public bool dialogeSkip = false;
    [HideInInspector] public bool canPlayNextDialoge = true;
    void Start()
    {
        dialogeText.text = string.Empty;
        dialogeWindow.SetActive(false);
    }

    void Update()
    {
        
    }

    public IEnumerator DialogeShow(string textToEnter)
    {

        dialogeWindow.SetActive(true);

        dialogeFinished = false;

        yield return new WaitForSeconds(0.1f);

        dialogeSkip = false;
        dialogeFinished = true;
        dialogeWindow.SetActive(false);
    }


}
