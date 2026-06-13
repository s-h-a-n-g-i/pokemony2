using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSongStart : MonoBehaviour
{
    private EventInstance bgsound;

    private EventInstance fightSound;
    void Start()
    {
        bgsound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.bgMusic);
        fightSound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.fightMusic);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "FightNew" || SceneManager.GetActiveScene().name != "FinalBoss")
        {
            fightSound.stop(STOP_MODE.IMMEDIATE);
            PLAYBACK_STATE playbackState;
            bgsound.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                bgsound.start();
            }
        }
        else if(SceneManager.GetActiveScene().name == "FightNew")
        {
            bgsound.stop(STOP_MODE.IMMEDIATE);
            PLAYBACK_STATE playbackState;
            fightSound.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                fightSound.start();
            }
        }
        
    }
}
