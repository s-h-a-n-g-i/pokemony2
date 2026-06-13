using TMPro;
using UnityEngine;

public class TextBeforeFight : MonoBehaviour
{
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        string s;
        if (_NPCManager.Instance.isItTrainer)
        {
            s = _NPCManager.Instance.TrainerName;
        }
        else 
        {
            s = _FightManager.Instance.EnemyPokemon.basicName;
        }

        text.text = "YOU ARE ATTACKED BY " + s;
    }

    
}
