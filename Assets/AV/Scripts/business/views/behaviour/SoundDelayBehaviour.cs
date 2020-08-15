using UnityEngine;
using System.Collections;

public class SoundDelayBehaviour : MonoBehaviour
{
    public float delaySeconds = 0f;

    private AudioSource sound;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
        sound.enabled = false;
    }

    void OnEnable()
    {
        if (sound == null)
            return;

        if (delaySeconds <= 0)
        {
            sound.enabled = true;
        }
        else
        {
            StartCoroutine(playSound());
        }
    }

    void OnDisable()
    {
        if (sound == null)
            return;

        sound.enabled = false;
    }

    IEnumerator playSound()
    {
        yield return new WaitForSeconds(delaySeconds);
        sound.enabled = true;
    }

}
