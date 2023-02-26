using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0f, 1f)]
    float masterVolume = 1;
    [Range(0f, 1f)]
    float sfxVolume = 1;
    [Range(0f, 1f)]
    float ambientVolume = 1;
    [Range(0f, 1f)]
    float musicVolume = 1;
    public static AudioManager instance { get; private set; }
    public EventInstance openWindZone;
    public EventInstance menuMusic;
    public EventInstance inGameMusic;

    public float windVolume = 1.0f; 
    public Bus masterBus;
    public Bus ambientBus;
    public Bus sfxBus;
    public Bus musicBus;
    void Awake()
    {
        if (instance == null) instance = this;
        // create event instances
        openWindZone = CreateEventInstance(FModEvents.instance.openWindZone);
        menuMusic = CreateEventInstance(FModEvents.instance.MenuMusic);
        menuMusic.setParameterByName("Loop", 1);
        inGameMusic = CreateEventInstance(FModEvents.instance.InGameMusic);
        inGameMusic.setParameterByName("Loop", 1);
               
    }

    private void Start()
    {
        
    }

    public void SetInstanceParameter(float value, string name, EventInstance instance)
    {
        instance.setParameterByName(name, value);
    }

    public void PlayOneShot(EventReference _event, Vector3 position)
    {
        RuntimeManager.PlayOneShot(_event, position);
    }

    public void setVolume()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        ambientBus.setVolume(ambientVolume);
    }
    public void changeMusicVolume(float music)
    {
        //musicVolume = music;
        musicBus.setVolume(music);
    }
    public void changeSFXVolume(float sfx)
    {
        //sfxVolume = sfx;
        sfxBus.setVolume(sfx);
    }
    public void changeAmbientVolume(float ambient)
    {
        //ambientVolume = ambient;
        ambientBus.setVolume(ambient);
    }

    public void changeMasterVolume(float master)
    {
        //masterVolume = master;
        masterBus.setVolume(master);
    }

    public EventInstance CreateEventInstance(EventReference eventRef) {
        return RuntimeManager.CreateInstance(eventRef);
    }
}
