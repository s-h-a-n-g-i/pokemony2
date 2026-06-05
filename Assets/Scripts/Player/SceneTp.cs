using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTp : MonoBehaviour
{
    [Header("Teleport Changes")]
    [SerializeField] private string sceneNameToTeleport;
    [Header("kordy do tp")]
    [SerializeField] private float x;
    [SerializeField] private float y;
    [Header("nie dotykaj tego bo ci jajo ukrece")]
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
        _PlayerSave.Instance._playerPosition = new Vector3(x, y, playerTransform.position.z);
        _PlayerSave.Instance._sceneName = sceneNameToTeleport;
        yield return new WaitForSeconds(0.6f);
        _PlayerSave.Instance.placed = false;
        SceneManager.LoadScene(sceneNameToTeleport);
    }
}
