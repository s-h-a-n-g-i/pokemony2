using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 prevPos, nextPos;
    private float speed = 0.2f;

    private bool hasRunningShoes = false;

    private bool placed = false;


    private void OnEnable()
    {
        if(!placed)
            if (PlayerSave._playerPosition != null && PlayerSave._sceneName != SceneManager.GetActiveScene().name) 
            {
                placed = true;
                transform.position = PlayerSave._playerPosition;
            }
    }

    void Update()
    {
        if (!isMoving)
            PlayerSaveUpdate();
        SprintCheck();
        MovementCheck();
    }

    private void PlayerSaveUpdate() 
    {
        PlayerSave._playerPosition = transform.position;
        PlayerSave._sceneName = SceneManager.GetActiveScene().name;
    }

      
    private void SprintCheck() 
    { 
        if (!hasRunningShoes) return;

        if (Input.GetKey(KeyCode.X))
            speed = 0.1f;
        else 
            speed = 0.2f;

    }

    private void MovementCheck() 
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isMoving && WallTest(Vector3.up))
        {
            isMoving = true;
            StartCoroutine(Move(Vector3.up));
        }

        if (Input.GetKey(KeyCode.DownArrow) && !isMoving && WallTest(Vector3.up))
        {
            isMoving = true;
            StartCoroutine(Move(Vector3.down));
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isMoving && WallTest(Vector3.up))
        {
            isMoving = true;
            StartCoroutine(Move(Vector3.right));
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving && WallTest(Vector3.up))
        {
            isMoving = true; 
            StartCoroutine(Move(Vector3.left)); 
        }
    }


    private IEnumerator Move(Vector3 dir) 
    {
        float leftTime = 0;

        prevPos = transform.position;
        nextPos = prevPos + dir;

        while (leftTime < speed) 
        {
            transform.position = Vector3.Lerp(prevPos, nextPos, (leftTime / speed));
            leftTime += Time.deltaTime;
            yield return null;
        }

        transform.position = nextPos;

        isMoving = false;
    }

    private bool WallTest(Vector3 dir)
    {
        Ray raycastCheckObject = new Ray(transform.position, dir);

        if (Physics.Raycast(raycastCheckObject, out RaycastHit hit, 1)) 
            if (hit.collider.gameObject.tag == "Wall") return false;
        return true;
    }














}
