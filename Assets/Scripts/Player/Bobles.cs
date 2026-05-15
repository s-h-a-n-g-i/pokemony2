using System.Collections;
using UnityEngine;

public class Bobles : MonoBehaviour
{

    [Header("scene")]
    [SerializeField] private SpriteRenderer sr;

    [Header("arts")]
    [SerializeField] private Sprite love;
    [SerializeField] private Sprite dots;
    [SerializeField] private Sprite question;
    [SerializeField] private Sprite exclamation;
    [SerializeField] private Sprite none;

    public IEnumerator loveBobel()
    {
        sr.sprite = love;
        yield return new WaitForSeconds(0.6f);
        sr.sprite = none;
    }
    public IEnumerator dotsBobel()
    {
        sr.sprite = dots;
        yield return new WaitForSeconds(0.6f);
        sr.sprite = none;
    }
    public IEnumerator questBobel()
    {
        sr.sprite = question;
        yield return new WaitForSeconds(0.6f);
        sr.sprite = none;
    }
    public IEnumerator exclBobel()
    {
        sr.sprite = exclamation;
        yield return new WaitForSeconds(0.6f);
        sr.sprite = none;
    }
}
