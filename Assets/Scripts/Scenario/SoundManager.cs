using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource fxSource;
    public AudioSource musicSource;
    public GameObject btnOn;
    public GameObject btnOff;
    private bool on;
    


    public static SoundManager instance = null; //Static permite que outros Scripts possam chamar seus métodos
    
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }

    }

    public void PlaySound(AudioClip clip) {
        fxSource.clip = clip;
        fxSource.Play ();
    }

    public void muteUnmute()
    {
        on = !on;
    
        switch (on)
    {
        case true:

        fxSource.Stop();
        musicSource.Stop();
        btnOn.SetActive(false);
        btnOff.SetActive(on);
        break;

        case false:

        fxSource.Play();
        musicSource.Play();
        btnOn.SetActive(true);
        btnOff.SetActive(false);
        break;
    }

    }
}