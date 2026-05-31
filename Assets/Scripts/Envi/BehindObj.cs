using Unity.VisualScripting;
using UnityEngine;

public class BehindObj : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private SpriteRenderer sr;
    void Start()
    {
        player = GameObject.Find("Player");  
        if(sr==null)
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
