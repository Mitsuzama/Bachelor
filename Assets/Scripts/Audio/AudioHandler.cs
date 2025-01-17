using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public static AudioHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    /**
     * @brief Functie responsabila de redarea audioului din joc, in special a melodiei de fundal
     * 
     * @param clip          clipul audio ce trebuie redat
     * @param timeToPlay    durata de timp pentru care trebuie redat videoclipul
     */
    public IEnumerator Play(AudioClip clip, float timeToPlay)
    {
        if(clip != null || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        
        if (timeToPlay > 0)
        {
            yield return new WaitForSeconds(timeToPlay);
            Stop();
        }
    }

    /**
     * @brief Functie responsabila de oprirea audioului din joc
     */
    public void Stop()
    {
        audioSource.Stop();
    }

    /**
     * @brief Functie responsabila de oprirea temporara a audioului din joc
     */
    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    /**
     * @brief Functie responsabila de marirea volumului
     */
    public void VolumeUp(float vol)
    {
        audioSource.volume += vol;
        audioSource.volume = Mathf.Clamp01(audioSource.volume);
    }

    /**
     * @brief Functie responsabila de scaderea volumului
     */
    public void VolumeDown(float vol)
    {
        audioSource.volume -= vol;
        audioSource.volume = Mathf.Clamp01(audioSource.volume);
    }
}
