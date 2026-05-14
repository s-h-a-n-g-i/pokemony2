using Unity.VisualScripting;
using UnityEngine;

public class BehindObj : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    void Start()
    {
        player = GameObject.Find("Player");  
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        Transform pt = player.transform;

        if (pt.position.y >= transform.position.y)
            sr.sortingOrder = 2;
        else
            sr.sortingOrder = 0;

    }
}
