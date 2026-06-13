using UnityEngine;

public class SquareToCharacter : MonoBehaviour
{
    [SerializeField] Sprite female;
    [SerializeField] Sprite male;

    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (_PlayerSave.Instance.male) 
            sr.sprite = male;
        else sr.sprite = female;
        
    }
}
