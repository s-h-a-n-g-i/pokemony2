using TMPro;
using UnityEngine;

public class WhatWillDo : MonoBehaviour
{
    [SerializeField] private TMP_Text WhatWillDoText;
    private void Start()
    {
        WhatWillDoText.GetComponent<TMP_Text>();
    }
    void Update()
    {
        WhatWillDoText.text = "What will <b>" + _PokemonEQ.Instance.ActivePokemon.basicName + "</b> do?";
    }
}
