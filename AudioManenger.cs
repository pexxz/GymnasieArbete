using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManenger : MonoBehaviour
{
    #region static Instance
    private static AudioManenger instance;
    public static AudioManenger Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<AudioManenger>();
                if(instance == null)
                {
                    instance = new GameObject("spawn AudioManenger", typeof(AudioManenger)).GetComponent<AudioManenger>();
                }
            }

            return instance;
        }

        private set
        {
            instance = value;
        }
    }
    #endregion
    //Libaray for audio in project
    #region Audio Library
    public AudioClip playerJumpSfx;
    public AudioClip spikeTrapSfx;
    public AudioClip collectDiamondSfx;
    public AudioClip FinishSfx;
    public AudioClip GameOverSfx;

    public AudioClip music01;
    public AudioClip music02;
    #endregion

    #region Fields Private
    private AudioSource musicSource01;
    private AudioSource musicSource02;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;
    #endregion
    #region AUDIO MANENGER
    private void Awake()
    {
        //Make sure we dont destroy this Instance
        DontDestroyOnLoad(this.gameObject);

        //Create Audio Sources, and save them as refrences
        musicSource01 = gameObject.AddComponent<AudioSource>();
        musicSource02 = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        //Loop Music Tracks
        musicSource01.loop = true;
        musicSource02.loop = true;
    }
    //
    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource01 : musicSource02;

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        //Determin what source is playing
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource01 : musicSource02;
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        //Determin what source is playing
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource01 : musicSource02;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource02 : musicSource01;

        //Sawp the source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        // Set the field of the audio source, then start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        //Make sure the source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;
        //Fade Out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        //Fade In
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource NewSource, float transitionTime)
    {
        float t = 0.0f;

        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            NewSource.volume = (t / transitionTime);
            yield return null;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource01.volume = volume;
        musicSource02.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    #endregion
}
