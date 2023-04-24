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
    public float timeToPlay;

    private void OnEnable()
    {
        StartCoroutine(nameof(PlayAudio));
    }

    private IEnumerator PlayAudio()
    {
        while (AudioManager.instance == null)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(AudioManager.instance.PlayAudio(audioToPlay, timeToPlay));
    }

    public void StopAudio()
    {
        AudioManager.instance.StopAudio();
    }

    private void OnDisable()
    {
        AudioManager.instance.StopAudio();
    }
}
