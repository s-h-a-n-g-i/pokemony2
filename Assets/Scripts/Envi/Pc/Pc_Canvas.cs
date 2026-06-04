using UnityEngine;

public class Pc_Canvas : MonoBehaviour
{
    private PlayerMovement player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        player.canMove = false;

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            player.canMove = true;
            gameObject.SetActive(false);
        }
    }
}
