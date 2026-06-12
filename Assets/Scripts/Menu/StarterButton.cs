using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarterButton : MonoBehaviour
{
    [SerializeField] PokemonSO Starter;
    [SerializeField] PokemonSO Starter2;
    [SerializeField] PokemonSO Starter3;
    [SerializeField] Image imageStarter;
    [SerializeField] TMP_Text nameStarter;

    private void Start()
    {
        imageStarter.sprite = Starter.image;
        nameStarter.text = Starter.basicName;
        if (_PlayerSave.Instance.playerName == "Shangi" || _PlayerSave.Instance.playerName == "Kami")
        {
            if (Starter2 == null) { Destroy(gameObject); }
            else
            {
                imageStarter.sprite = Starter2.image;
                nameStarter.text = Starter2.basicName;
            }
        }
    }

    public void ButtonClicked() 
    {
        if (_PlayerSave.Instance.playerName == "Shangi" || _PlayerSave.Instance.playerName == "Kami")
        {
            _PokemonEQ.Instance.EqPokemons[0] = new Pokemon(Starter2, 3);
        }
        else
            _PokemonEQ.Instance.EqPokemons[0] = new Pokemon(Starter, 3);

        _PokemonEQ.Instance.EqPokemons[1] = new Pokemon(Starter2, 3);
        _PokemonEQ.Instance.EqPokemons[2] = new Pokemon(Starter3, 3);
        for(int i = 0; i < 7*12;i++)
            _PokemonEQ.Instance.AllHavePokemons.Add(new Pokemon(Starter2, 3));
        //_PokemonEQ.Instance.EqPokemons[2] = new Pokemon(Starter, 3);
        //_PokemonEQ.Instance.EqPokemons[3] = new Pokemon(Starter, 3);
        //_PokemonEQ.Instance.EqPokemons[4] = new Pokemon(Starter, 3);

        //SceneManager.LoadScene("SampleScene");
    }
}
