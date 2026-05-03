using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;
    private GameObject player;
    private Transform playerTransform;
    private PlayerMovement movement;
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        movement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Debug.Log(movement.isMoving);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movement.StopAllCoroutines();
        playerTransform.position = teleportTo.position;
        movement.isMoving = false;
    }
}
