using UnityEngine;
using UnityEngine.InputSystem.Utilities;

[System.Serializable]
public class DialogeLine
{
    public string whoSayes;
    [TextArea(10,30)]
    public string whatSayes;
}
