using UnityEngine;

public class ButtonSoundPressed : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pressedButton, transform.position);
    }

    public void SelectSound()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.selectButton, transform.position);
    }
}
