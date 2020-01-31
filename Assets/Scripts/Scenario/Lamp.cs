using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lamp : MonoBehaviour {
    
    [Header("Image of the Lamp")]
    public Sprite[] imgObject;
    private SpriteRenderer spriteRenderer;
    private bool on;

    [Header("Audio")]
    public AudioClip fxLamp;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void Interaction() {
        on = !on;

        switch(on) {
            case true:
                spriteRenderer.sprite = imgObject[1];
                SoundManager.instance.PlaySound(fxLamp);
                break;

            case false:
                spriteRenderer.sprite = imgObject[0];
                SoundManager.instance.PlaySound(fxLamp);
                break;
        }
    }
}