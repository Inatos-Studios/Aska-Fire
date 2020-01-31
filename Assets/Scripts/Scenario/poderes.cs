using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class poderes : MonoBehaviour
{
    public GameObject ruby;
    public GameObject rubyHud;
    private bool portal = false;
    public GameObject portalFase;
    private SpriteRenderer sprite;
    public GameObject esmeralda;
    public GameObject esmeraldaHud;
    private Player playerScript;
    private _GameController _GameController;
    public Button button;


    void Start()
    {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        playerScript = FindObjectOfType(typeof(Player)) as Player;
        sprite = GetComponent<SpriteRenderer> ();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire2") && portal == true)
        {
            portalFase.SetActive(true);
            rubyHud.SetActive(false);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ruby")
    { 
        rubyHud.SetActive(true);
        Destroy(ruby);
        portal = true;
        
    }
        if (col.gameObject.name == "Esmeralda")
        {
            Destroy(esmeralda);
            esmeraldaHud.SetActive(true);

        }

    }
        IEnumerator OnTriggerStay2D(Collider2D other) {
        
        if(other.CompareTag ("Portal"))
        {
            PlayerPrefs.SetInt("faseSalva", SceneManager.GetActiveScene().buildIndex + 1);
           SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        for (float i = 0f; i < 1f; i += 0.1f){
            sprite.enabled = false;
            yield return new WaitForSeconds (0.3f);
            sprite.enabled = true;
            yield return new WaitForSeconds (0.3f);
        }
        }
        }


}

































    //
    //    public void planoEspiritual()
    //{   
     //       if (plano = true && playerScript.Grounded == true){
       //     GetComponent<Rigidbody2D> ().gravityScale = 0;
         //   GetComponent<CapsuleCollider2D> ().isTrigger = true;
           // GetComponent<SpriteRenderer> ().color = new Color (1,1,1,.5f);
           // playerScript.jumpForce = 0f;


        
        //}   else {
         //   GetComponent<Rigidbody2D> ().gravityScale = 1;
          //  GetComponent<CapsuleCollider2D> ().isTrigger = false;
           // GetComponent<SpriteRenderer> ().color = new Color (1,1,1,1);
            //playerScript.jumpForce = 110f;
          // }
   // }
    
