using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }

    [field: Header("Ingame Sounds")]
    [field: SerializeField] public EventReference walkingSound { get; private set; }
    [field: SerializeField] public EventReference healingSound { get; private set; }
    [field: Header("Fight Sounds")]
    [field: SerializeField] public EventReference Catch { get; private set; }
    [field: SerializeField] public EventReference Death { get; private set; }
    [field: SerializeField] public EventReference hurt { get; private set; }
    [field: SerializeField] public EventReference missed { get; private set; }
    [field: SerializeField] public EventReference punch { get; private set; }
    [field: SerializeField] public EventReference swap { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference fightMusic { get; private set; }
    [field: SerializeField] public EventReference bgMusic { get; private set; }

    [field: Header("UI")]
    [field: SerializeField] public EventReference pressedButton { get; private set; }
    [field: SerializeField] public EventReference selectButton { get; private set; }
    [field: SerializeField] public EventReference dialogeSound { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Za duzo manageruw audio");
            return;
        }
        Instance = this;
    }


}
