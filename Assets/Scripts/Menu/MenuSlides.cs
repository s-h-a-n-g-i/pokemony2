using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSlides : MonoBehaviour
{
    [SerializeField] private PokemonCheck check;
    [SerializeField] private int state = 0;
    [SerializeField] Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("state", state);

        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Escape))
        {
            if (state == 0)
            {
                state = 1;
                check.chosenPokemon = _PokemonEQ.Instance.EqPokemons[0];
            }
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


    public void LeaveToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
