using UnityEngine;
using UnityEngine.UI;

public class Pc_SlotButton : MonoBehaviour
{
    [SerializeField] private Pc_Grid grid;
    [SerializeField] private int slotIndex;
    [SerializeField] private Pc_SlotButton nextSlotButton;
    private Image spriteRenderer;
    public Sprite NonePokemonSprite;
    public bool slotEquiped = false;
    private void Start()
    {
        spriteRenderer = GetComponent<Image>();
    }

    void Update()
    {
        if (_PokemonEQ.Instance.EqPokemons[slotIndex] != null && _PokemonEQ.Instance.EqPokemons[slotIndex].basicName != string.Empty)
        {
            spriteRenderer.sprite = _PokemonEQ.Instance.EqPokemons[slotIndex].image;
            slotEquiped = true;
        }
        else 
        {
            spriteRenderer.sprite = NonePokemonSprite;
            slotEquiped = false;
        }
    }

    public void PressedButton() 
    {
        if (slotIndex == 0 && !nextSlotButton.slotEquiped) return;
        if (!slotEquiped) return;
        grid.AddPokemonToList(_PokemonEQ.Instance.EqPokemons[slotIndex]);
        _PokemonEQ.Instance.AllHavePokemons.Add(_PokemonEQ.Instance.EqPokemons[slotIndex]);
        nextPokemon();
    }

    public void nextPokemon() 
    {
        _PokemonEQ.Instance.EqPokemons[slotIndex] = null;
        if (slotIndex == 4) return;
        _PokemonEQ.Instance.EqPokemons[slotIndex] = _PokemonEQ.Instance.EqPokemons[slotIndex + 1];
        nextSlotButton.nextPokemon();
    }

}
