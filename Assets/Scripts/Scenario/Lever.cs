using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {
    
    [Header("Image of the Lever")]
    public Sprite[] imgObject;
    private SpriteRenderer spriteRenderer;
    private bool on;
    private Gate gateScript;

    [Header("Audio")]
    public AudioClip fxLever;

    void Start() {
        gateScript = FindObjectOfType(typeof(Gate)) as Gate;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interaction() {
        on = !on;

        switch(on) {
            case true:
                spriteRenderer.sprite = imgObject[1];
                gateScript.anim.SetBool("Open", true);
                gateScript.anim.SetBool("Close", false);
                SoundManager.instance.PlaySound(fxLever);
                break;

            case false:
                spriteRenderer.sprite = imgObject[0];
                gateScript.anim.SetBool("Close", true);
                gateScript.anim.SetBool("Open", false);
                SoundManager.instance.PlaySound(fxLever);
                break;
        }
    }
}