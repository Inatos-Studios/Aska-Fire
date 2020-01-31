using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {
    
    private Rigidbody2D rb2d;
    private Animator anim;
    private Player playerScript;

    void Start() {
        playerScript = FindObjectOfType(typeof(Player)) as Player;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player") {
            playerScript.playerRb.AddForce(new Vector2(0,370));
            anim.SetBool("JumpOn", true);
            anim.SetBool("JumpOff", false);
        
        } else if(other.tag != "Player") {
            anim.SetBool("JumpOff", true);
            anim.SetBool("JumpOn", false);
        }
    }
}