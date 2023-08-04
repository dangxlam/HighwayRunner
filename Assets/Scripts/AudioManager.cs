using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Start()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }

        PlaySound("MainSound");
    }

    public void PlaySound(string name)
    {
        foreach (var sound in sounds)
        {
            if(sound.name == name)
                sound.source.Play();
        }
    }

}
