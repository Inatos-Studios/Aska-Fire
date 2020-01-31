using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    
    public Sprite[] sprites;
    public Image lifeBar;
    public static HUD instance;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(instance);
        }
    }

    public void RefreshLife(int playerHealth) {
        lifeBar.sprite = sprites[playerHealth];
    }
}