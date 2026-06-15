using UnityEngine;

public class ButtonSoundPressed : MonoBehaviour
{
    public void PlaySound() 
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pressedButton,transform.position);
    }
}
