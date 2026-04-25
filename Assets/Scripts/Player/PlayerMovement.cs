using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 prevPos, nextPos;
    private float speed = 0.2f;
    private Animator animator;
    private Vector3 dirwalk;

    private bool hasRunningShoes = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dirwalk = Vector3.down;

        if (!PlayerSave.placed)
            if (PlayerSave._playerPosition != Vector3.zero && PlayerSave._sceneName == SceneManager.GetActiveScene().name)
            {
                PlayerSave.placed = true;
                transform.position = PlayerSave._playerPosition;
            }
    }


    void Update()
    {
        
        SprintCheck();
        MovementCheck();
        PlayerSave._sceneName = SceneManager.GetActiveScene().name;
        animator.SetBool("isWalking", isMoving);
        animator.SetFloat("InputX", dirwalk.x);
        animator.SetFloat("InputY", dirwalk.y);
        if (!isMoving)
        {
            animator.SetFloat("LastInputX", dirwalk.x);
            animator.SetFloat("LastInputY", dirwalk.y);

        }
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

        if (Input.GetKey(KeyCode.DownArrow) && !isMoving && WallTest(Vector3.down))
        {
            isMoving = true;
            if(JumpDownTest(Vector3.down))
                StartCoroutine(Move(Vector3.down));
            else
                StartCoroutine(JumpDown());
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isMoving && WallTest(Vector3.right))
        {
            isMoving = true;
            StartCoroutine(Move(Vector3.right));
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving && WallTest(Vector3.left))
        {
            isMoving = true; 
            StartCoroutine(Move(Vector3.left)); 
        }
    }


    private IEnumerator Move(Vector3 dir)
    {
        dirwalk = dir;
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
        PlayerSave._playerPosition = nextPos;
    }

    private IEnumerator JumpDown()
    {
        Vector3 dir = Vector3.down;
        dirwalk = dir;
        float leftTime = 0;

        prevPos = transform.position;
        nextPos = prevPos + dir*2;

        while (leftTime < speed)
        {
            transform.position = Vector3.Lerp(prevPos, nextPos, (leftTime / speed));
            leftTime += Time.deltaTime;
            yield return null;
        }

        transform.position = nextPos;
        isMoving = false;
        PlayerSave._playerPosition = nextPos;
        //Debug.Log(PlayerSave._playerPosition);
    }

    private bool WallTest(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir / 2, dir ,0.2f);
        if (hit)
            if(hit.collider.gameObject.tag == "Wall") return false;

        if(dir == Vector3.down) return true;

        if (hit)
            if (hit.collider.gameObject.tag == "Jump") return false;
        
        return true;
    }

    private bool JumpDownTest(Vector3 dir)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir / 2, dir, 0.2f);
        if (hit)
            if (hit.collider.gameObject.tag == "Jump") return false;
        return true;
    }













}
