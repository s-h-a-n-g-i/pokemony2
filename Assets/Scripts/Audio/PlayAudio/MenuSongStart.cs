using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSongStart : MonoBehaviour
{
    private EventInstance bgsound;

    private EventInstance fightSound;
    private EventInstance caveSound;
    private EventInstance lavaSound;
    private EventInstance poisonSound;
    void Start()
    {
        caveSound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.caveMusic);
        lavaSound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.lavaMusic);
        poisonSound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.poisonMusic);
        bgsound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.bgMusic);
        fightSound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.fightMusic);
    }

    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene != "FinalBoss")
            switch(scene)
            {
                case "lvl1":
                    stopAll(4);
                    caveMusic();
                    break;
                case "lvl2":
                    stopAll(2);
                    lavaMusic();
                    break;
                case "lvl4":
                    stopAll(3);
                    poisonMusic();
                    break;
                case "FightNew":
                    stopAll(1);
                    fightMusic();
                    break;
                default:
                    stopAll(0);
                    bgMusic();
                    break;
            }
            else
            stopAll();
    }
    private void stopAll(int s = -1)
    {
        if (s != 0) bgsound.stop(STOP_MODE.IMMEDIATE);
        if (s != 1) fightSound.stop(STOP_MODE.IMMEDIATE);
        if (s != 2) lavaSound.stop(STOP_MODE.IMMEDIATE);
        if (s != 3) poisonSound.stop(STOP_MODE.IMMEDIATE);
        if (s != 4) caveSound.stop(STOP_MODE.IMMEDIATE);
    }
    private void bgMusic()
    {
        PLAYBACK_STATE playbackState;
        bgsound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            bgsound.start();
        }
    }

    private void fightMusic()
    {
        PLAYBACK_STATE playbackState;
        fightSound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            fightSound.start();
        }
    }
    private void caveMusic()
    {
        PLAYBACK_STATE playbackState;
        caveSound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            caveSound.start();
        }
    }

    private void poisonMusic()
    {
        PLAYBACK_STATE playbackState;
        poisonSound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            poisonSound.start();
        }
    }
    private void lavaMusic()
    {
        PLAYBACK_STATE playbackState;
        lavaSound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            lavaSound.start();
        }
    }


}
