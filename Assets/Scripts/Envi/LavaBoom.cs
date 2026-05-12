using UnityEngine;

public class LavaBoom : MonoBehaviour
{
    [SerializeField] private Vector2 timeDelay;
    private Animator animator;
    private float s;
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > s) 
            Boom();
        
    }

    private void Boom() 
    {
        s = Random.Range(timeDelay.x, timeDelay.y)+Time.timeSinceLevelLoad;
        animator.SetTrigger("bum");
    }
}
