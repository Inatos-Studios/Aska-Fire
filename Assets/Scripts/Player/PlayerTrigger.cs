using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {
    
    private Player playerScript;
    private GameObject HUD;
    
    [Header("Audio")]
    public AudioClip fxCoin;
    public AudioClip fxEatApple;

    void Start() {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Enemy")) {
            playerScript.DamagePlayer();
        }
        
        if (other.CompareTag("Lava")) {
            playerScript.DamageLava();
        }

        if (other.CompareTag("Apple")) {
            Destroy (other.gameObject);
            SoundManager.instance.PlaySound(fxEatApple);
            GameObject.Find("Player").GetComponent<Player>().health++;
        }
        if (other.CompareTag("KingBoss")) {
            playerScript.DamagePlayer();
        }
    }
}