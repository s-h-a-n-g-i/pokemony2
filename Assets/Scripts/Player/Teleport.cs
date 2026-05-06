using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;
    [SerializeField] private Animator blinkingAnim;
    private GameObject player;
    private Transform playerTransform;
    private PlayerMovement movement;
    void Start()
    {
        blinkingAnim = GameObject.Find("BinkTransform").GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        movement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movement.StopAllCoroutines();
        StartCoroutine(Tp());
    }

    IEnumerator Tp() 
    {
        
        movement.canMove = false;
        movement.StopMoving();
        blinkingAnim.SetBool("black", true);
        yield return new WaitForSeconds(0.6f);
        playerTransform.position = teleportTo.position;
        blinkingAnim.SetBool("black", false);
        yield return new WaitForSeconds(0.5f);
        movement.canMove = true;
        movement.isMoving = false;
    }

}
