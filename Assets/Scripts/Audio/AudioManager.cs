using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Range(0, 1)]
    public float musicVolume = 1f;
    [Range(0, 1)]
    public float sfxVolume = 1f;
    [Range(0, 1)]
    public float uiVolume = 1f;

    private Bus musicBus;
    private Bus sfxBus;
    private Bus uiBus;

    private void Awake()
    {

        if (Instance != null) 
        {
            return;
            //Debug.LogError("Za duzo manageruw audio");
        }
        Instance = this;


        musicBus = RuntimeManager.GetBus("bus:/MUSIC");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        uiBus = RuntimeManager.GetBus("bus:/UI");
    }

    private void Update()
    {
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        uiBus.setVolume(uiVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos) 
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference) 
    { 
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
