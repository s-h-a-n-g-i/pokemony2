using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private UnityEvent interacted;

    private GameObject player;
    private PlayerMovement playerMovement;
    private void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position + playerMovement.lastDir / 2, playerMovement.lastDir, 0.2f);
            if (hit)
                if (hit.collider.gameObject == gameObject)
                    interacted.Invoke();
        }
    }
}
