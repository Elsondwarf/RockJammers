using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private EventInstance musicEventInstance;
    
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More then one instance of AudioManager!");
        }
        Instance = this;
    }

    private void Start()
    {
        InitializeMusic(FMODEvents.Instance.mainMenuMusic);
    }

    public void PlayOneShot(EventReference soundEvent, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(soundEvent, worldPosition);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public void SetMusicSoundtrack(MusicEnum ctx)
    {
        musicEventInstance.setParameterByName("Scene", (float)ctx);
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }
}
