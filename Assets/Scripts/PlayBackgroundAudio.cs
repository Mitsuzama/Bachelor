using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief   Script pentru tularea sunetului de fundal dintr-un magazin.
 *          Cand GameObject-ul script-ului este activat, acesta porneste o rutina numita "PlayAudio" folosind functia StartCoroutine().
 *          Rutina asteapta ca instanta de AudioManager sa fie initializata (asteapta sfarsitul urmatorului cadru) si apeleaza functia PlayAudio() a AudioManager-ului.
 */
public class PlayBackgroundAudio : MonoBehaviour
{
    [Tooltip("Numele clipului care va fi rulat")]
    public AudioClip audioToPlay;
    [SerializeField]
    private float timeToPlay = Mathf.Infinity;

    private void OnEnable()
    {
        StartCoroutine(nameof(PlayAudio));
    }

    private IEnumerator PlayAudio()
    {
        while (AudioHandler.instance == null)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(AudioManager.instance.Play(audioToPlay, timeToPlay));
    }

    public void StopAudio()
    {
        AudioHandler.instance.Stop();
    }

    public void PauseAudio()
    {
        AudioHandler.instance.Pause();
    }

    public void IncreaseAudio(float vol)
    {
        AudioHandler.instance.VolumeUp(vol);
    }

    public void DecreaseAudio(float vol)
    {
        AudioHandler.instance.VolumeDown(vol);
    }

    private void OnDisable()
    {
        AudioHandler.instance.Stop();
    }
}
