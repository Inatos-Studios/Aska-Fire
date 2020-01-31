using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    private _GameController _GameController;

    [Header("Value of The Coin")]
    public int value;

    [Header("Audio")]
    public AudioClip fxCoin;

    void Start() {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
    }

    public void Colection() {
        _GameController.gold += value;
        SoundManager.instance.PlaySound(fxCoin);
        Destroy(this.gameObject);
    }
}