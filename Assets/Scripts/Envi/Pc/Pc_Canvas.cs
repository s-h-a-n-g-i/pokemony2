using UnityEngine;

public class Pc_Canvas : MonoBehaviour
{
    private PlayerMovement player;

    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        player.canMove = false;

        if (Input.GetKeyDown(KeyCode.X)) 
        {
            player.canMove = true;
            gameObject.SetActive(false);
        }
    }
}
