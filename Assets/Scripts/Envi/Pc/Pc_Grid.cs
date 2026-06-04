using UnityEngine;
using UnityEngine.UI;

public class Pc_Grid : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;

    private void OnEnable()
    {
        PokemonListSpawn();
    }

    private void OnDisable()
    {
        ResetPokemons();
    }

    private void PokemonListSpawn() 
    {
        foreach (Pokemon p in _PokemonEQ.Instance.AllHavePokemons)
        {
            GameObject s = Instantiate(buttonPrefab, transform);
            s.GetComponent<Pc_ListButton>().pokemon = p;
            s.GetComponent<Image>().sprite = p.image;
        }
    }


    private void ResetPokemons()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void AddPokemonToList(Pokemon p) 
    {
        GameObject s = Instantiate(buttonPrefab, transform);
        s.GetComponent<Pc_ListButton>().pokemon = p;
        s.GetComponent<Image>().sprite = p.image;
    }
}
