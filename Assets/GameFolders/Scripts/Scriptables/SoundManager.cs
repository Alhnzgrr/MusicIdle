using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] SoundData data;
    [SerializeField] int maxMultipleSound = 5;

    Dictionary<string, float> volume = new Dictionary<string, float>();
    Dictionary<string, AudioClip> clip = new Dictionary<string, AudioClip>();

    AudioSource[] audioSources;

    private void Awake()
    {
        Singleton(true);
    }

    private void Start()
    {
        Initiate();
    }

    void Initiate()
    {
        foreach (SoundClip soundClip in data.SoundClips)
        {
            volume.Add(soundClip.Name, soundClip.Volume);
            clip.Add(soundClip.Name, soundClip.Clip);
        }
        
        for (int i = 0; i < maxMultipleSound; i++)
        {
            GameObject newAudioSource =  new GameObject();
            newAudioSource.AddComponent<AudioSource>();
            newAudioSource.name = $"AudioSource {i}";
            newAudioSource.transform.parent = transform;
        }
        
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void Play(string name)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip[name];
                audioSource.volume = volume[name];
                audioSource.Play();
                break;
            }
        }
    }

    public void PlayOnIncrease(string name, float coefficient)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip[name];
                audioSource.volume = volume[name];
                StartCoroutine(IncreaseVolume(audioSource, coefficient));
                break;
            }
        }
    }

    public void PlayOnDecrease(string name, float coefficient)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip[name];
                audioSource.volume = volume[name];
                StartCoroutine(DecreaseVolume(audioSource, coefficient));
                break;
            }
        }
    }

    IEnumerator IncreaseVolume(AudioSource audioSource, float coefficient)
    {
        float clipLenght = audioSource.clip.length;
        float currentTime = clipLenght;

        audioSource.Play();

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime * coefficient;
            audioSource.volume -= currentTime / clipLenght;
            yield return null;
        }

        audioSource.volume = 0;
    }

    IEnumerator DecreaseVolume(AudioSource audioSource, float coefficient)
    {
        float clipLenght = audioSource.clip.length;
        float currentTime = clipLenght;

        audioSource.Play();

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime * coefficient;
            audioSource.volume += currentTime / clipLenght;
            yield return null;
        }

        audioSource.volume = 1;
    }
}
