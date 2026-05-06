using UnityEngine;

public class MenuSlides : MonoBehaviour
{

    [SerializeField] private int state = 0;
    [SerializeField] Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("state", state);

        if (Input.GetKey(KeyCode.X) && state == 0) 
        {
            state = 1;
        }
    }

    public void CloseMenu()
    {
        state = 0;
    }

    public void MaxRangeMenu()
    {
        state = 2;
    }

}
