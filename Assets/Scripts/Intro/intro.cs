using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Transform groundCheck;
    private bool Grounded = false;
    private Animator playerAnimator;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer> ();
        playerAnimator = GetComponent<Animator>();
    }
        IEnumerator OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag ("Portal"))
        {
           SceneManager.LoadScene("FASE1");
        for (float i = 0f; i < 1f; i += 0.1f){
            sprite.enabled = false;
            yield return new WaitForSeconds (0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds (0.1f);
        }
        }
        }



    void Update()
    {
        playerAnimator.SetBool("grounded", Grounded);
        Grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
