using UnityEngine;
using UnityEngine.UI;

public class SettingsSliders : MonoBehaviour
{
    Slider slider;

    [SerializeField] int settings = 0;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        switch (settings)
        {
            case 0:
                AudioManager.Instance.musicVolume = slider.value; break;
            case 1:
                AudioManager.Instance.sfxVolume = slider.value; break;
            case 2:
                AudioManager.Instance.uiVolume = slider.value; break;

        }
    }
}
