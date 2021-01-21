using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    public AudioMixer mixer;

    //synchronize handle of slider and mixer handle
    public void SetSoundLevel( float soundLevel)
    {
        mixer.SetFloat("MasterVolume", soundLevel);
    }
}
