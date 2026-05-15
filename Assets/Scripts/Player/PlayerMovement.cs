using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false, canMove = true;
    [HideInInspector] public Vector3 prevPos, nextPos, lastDir;
    private float speed = 0.2f;
    private Animator animator;
    private Vector3 dirwalk;

    private bool hasRunningShoes = false;

    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");

        animator = GetComponent<Animator>();
        PlayerSave.Instance._sceneName = SceneManager.GetActiveScene().name;

        if (!PlayerSave.Instance.placed)
            if (PlayerSave.Instance._playerPosition != Vector3.zero && PlayerSave.Instance._sceneName == SceneManager.GetActiveScene().name)
            {
                dirwalk = PlayerSave.Instance._playerRotation;
                lastDir = PlayerSave.Instance._playerRotation;
                PlayerSave.Instance.placed = true;
                transform.position = PlayerSave.Instance._playerPosition;
                animator.SetFloat("LastInputX", lastDir.x);
                animator.SetFloat("LastInputY", lastDir.y);
            }
    }


    void Update()
    {
        if (!canMove) return;
        SprintCheck();
        MovementCheck();
        animSet();
    }

    public void StopPlayer()
    {
        canMove = false;
        animator.SetBool("isWalking", false);
        StopAllCoroutines();
    }
    public void StartPlayer()
    {
        canMove = true;
        StopAllCoroutines();
    }

    private void animSet() 
    {
        animator.SetBool("isWalking", isMoving);
        animator.SetFloat("InputX", dirwalk.x);
        animator.SetFloat("InputY", dirwalk.y);
        PlayerSave.Instance._playerRotation = dirwalk;
    }

      
    private void SprintCheck() 
    { 
        if (!hasRunningShoes) return;

        if (Input.GetKey(KeyCode.Z))
            speed = 0.1f;
        else 
            speed = 0.2f;

    }

    private void MovementCheck() 
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isMoving)
        {
            lastDir = new Vector2(0,1);
            animator.SetFloat("LastInputX", lastDir.x);
            animator.SetFloat("LastInputY", lastDir.y);

            if (WallTest(Vector3.up))
            {
                isMoving = true;
                StartCoroutine(Move(Vector3.up));
            }
            
        }

        if (Input.GetKey(KeyCode.DownArrow) && !isMoving)
        {
            lastDir = new Vector2(0, -1);
            animator.SetFloat("LastInputX", lastDir.x);
            animator.SetFloat("LastInputY", lastDir.y);

            if (WallTest(Vector3.down))
            {
                isMoving = true;
                if (JumpDownTest(Vector3.down))
                    StartCoroutine(Move(Vector3.down));
                else
                    StartCoroutine(JumpDown());
            }

               
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isMoving)
        {
            lastDir = new Vector2(1, 0);
            animator.SetFloat("LastInputX", lastDir.x);
            animator.SetFloat("LastInputY", lastDir.y);

            if (WallTest(Vector3.right))
            {
                isMoving = true;
                StartCoroutine(Move(Vector3.right));
            }
                
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving)
        {
            lastDir = new Vector2(-1, 0);
            animator.SetFloat("LastInputX", lastDir.x);
            animator.SetFloat("LastInputY", lastDir.y);

            if (WallTest(Vector3.left))
            {
                isMoving = true;
                StartCoroutine(Move(Vector3.left));
            }
               
        }
    }

    public void StopMoving()
    {
        isMoving = false;
        animator.SetBool("isWalking", false);
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
        PlayerSave.Instance._playerPosition = nextPos;
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
        PlayerSave.Instance._playerPosition = nextPos;
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
