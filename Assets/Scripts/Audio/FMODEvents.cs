using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }

    [field: Header("Walking Sound")]
    [field: SerializeField] public EventReference walkingSound { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Za duzo manageruw audio");
        }
        Instance = this;
    }


}
