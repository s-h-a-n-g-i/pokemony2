using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarterButton : MonoBehaviour
{
    [SerializeField] PokemonSO Starter;
    [SerializeField] Image imageStarter;
    [SerializeField] TMP_Text nameStarter;

    private void Start()
    {
        imageStarter.sprite = Starter.image;
        nameStarter.text = Starter.basicName;
    }

    public void ButtonClicked() 
    {
        _PokemonEQ.Instance.EqPokemons[0] =  new Pokemon(Starter, 3);
        //_PokemonEQ.Instance.EqPokemons[1] = new Pokemon(Starter, 3);

        //SceneManager.LoadScene("SampleScene");
    }
}
