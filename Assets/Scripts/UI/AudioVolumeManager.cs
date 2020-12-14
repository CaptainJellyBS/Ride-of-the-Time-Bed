using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioVolumeManager : MonoBehaviour
{
    public static AudioVolumeManager Instance { get; private set; }
    public UnityEvent awakeEvents;
    private void Awake()
    {
        if(Instance != null) { Destroy(this); return; }
        Instance = this;
       
        DontDestroyOnLoad(this);       

        if (PlayerPrefs.HasKey("Volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            AudioListener.volume = 0.5f;
        }

        awakeEvents.Invoke();
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetPitch(float pitch)
    {
        foreach(AudioSource aS in GetComponentsInChildren<AudioSource>())
        {
            aS.pitch = pitch;
        }
    }
}
