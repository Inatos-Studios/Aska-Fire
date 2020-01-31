using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    [Header("Animation Gate")]
    public Animator anim;
    private bool open;

    [Header("Audio")]
    public AudioClip fxGate;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void Interaction() {
        open = !open;

        switch(open) {
            case true:
                anim.SetBool("Open", true);
                SoundManager.instance.PlaySound(fxGate);
                break;

            case false:
                anim.SetBool("Close", true);
                SoundManager.instance.PlaySound(fxGate);
                break;
        }
    }
}