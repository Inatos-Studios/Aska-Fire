using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    
    private _GameController _GameController;
    private SpriteRenderer sprite;
	private Animator playerAnimator;
    public Rigidbody2D playerRb;
    private bool invulnerable = false;
    private int idAnimation;
	private bool lookLeft;
    private bool isdead;
    private bool doubleJump = false;
    public static int mortes;
    private string cenaAtual;
    private static bool rewarded;

    [Header("Health")]
	public int health;

    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public bool Grounded = false;


    [Header("Attack")]
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerEnemy;

    [Header("Interaction")]
    public Transform hand;
    private Vector3 dir = Vector3.right;
    public LayerMask interaction;
    public GameObject objInteraction;
    public GameObject alertInteraction;
    public GameObject panelDead;
    public GameObject lampiao;

    [Header("Damage")]
    public GameObject damageText;
    
    [Header("Audio")]
    public AudioClip fxJump;
    public AudioClip fxOneLife;
    public AudioClip fxAttack;
    public AudioClip fxDeath;

    void Start() {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer> ();
        cenaAtual = SceneManager.GetActiveScene ().name;
        if (rewarded == true)
        {
            transform.position = lampiao.transform.position;
            health = 1;
            Time.timeScale = 1f;
            panelDead.SetActive(false);
            rewarded = false;
        }
        
    }

    void FixedUpdate() {
        HUD.instance.RefreshLife(health);
        Grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        PlayerInteraction();
    }

    void Update() 
    {
        if (Input.GetButtonDown("Fire1") && objInteraction == null)
        {
            playerAnimator.SetTrigger("attack");
            SoundManager.instance.PlaySound(fxAttack);
        }

        if (Input.GetButtonDown("Fire2") && objInteraction != null)
        {
          objInteraction.SendMessage("Interaction", SendMessageOptions.DontRequireReceiver);  
        }



        if(Input.GetButtonDown("Jump") && Grounded == true) 
        {
            playerRb.AddForce(new Vector2(0f, jumpForce));
            doubleJump = true;
            SoundManager.instance.PlaySound(fxJump);
        } 

        else if(Input.GetButtonDown("Jump") && doubleJump == true) 
        {   
            playerRb.AddForce(new Vector2(0f, jumpForce));
            doubleJump = false;
            SoundManager.instance.PlaySound(fxJump);               
        }       
        


        if (mortes == 3)
        {
            mortes = 0;
        }



        float h = Input.GetAxis("Horizontal");
        

        if ( h < 0 && !lookLeft) {
            flip();
        }
        else if (h > 0 && lookLeft) {
            flip();
        }

    	
    	else if(h != 0.3f) {
    	 				idAnimation = 1; 
    	} 
        else {
    					idAnimation = 0;
    	}
    
    if (h > 0.3f || h < -0.3f ){
        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);   
        idAnimation = 1;  
    } 	
    else{
        playerRb.velocity = new Vector2(h * 0, playerRb.velocity.y);
        idAnimation = 0;
    }
       	playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);
    }

     void flip() {
        if(isdead == false){
    	lookLeft = !lookLeft;
    	float x = transform.localScale.x;
    	x *= -1;
    	transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        dir.x = x;
        }
    }

//Animação de Dano Recebido
    IEnumerator DamageEffect() {
        for (float i = 0f; i < 1f; i += 0.1f){
            sprite.enabled = false;
            yield return new WaitForSeconds (0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds (0.1f);
        }
        
        invulnerable = false;
    }

//Invulnerabilidade e Dano Recebido
    public void DamagePlayer() {
        if (!invulnerable) {            
            invulnerable = true;
            playerAnimator.SetTrigger("hit");
            health--;
            StartCoroutine(DamageEffect());

            HUD.instance.RefreshLife(health);

            if (health == 1) {
            SoundManager.instance.PlaySound(fxOneLife);
            }

            if (health < 1) {
            StartCoroutine(AskaDeath());
            }
        }
    }
    
    public void DamageLava() {
        if (!invulnerable) {
            invulnerable = true;
            health = 0;
            StartCoroutine(DamageEffect());

            HUD.instance.RefreshLife(health);

            if (health < 1) {
            StartCoroutine(AskaDeath());
            }
        }
    }   

    void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    
    void OnDrawGizmosSelected() { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }
    
    void PlayerAttack() {
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll (attackCheck.position, radiusAttack, layerEnemy);
        for (int i = 0; i < enemiesAttack.Length; i++) {
            enemiesAttack [i].SendMessage ("DamageEnemy");
            enemiesAttack [i].SendMessage ("EnemyHit", "-5");
        }
    }

    IEnumerator AskaDeath(){
        isdead = true;
        playerAnimator.SetTrigger("Dead");
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        playerRb.velocity = new Vector2(0,0);
        mortes += 1;   
        yield return new WaitForSeconds (2.2f);
        Time.timeScale = 0f;
        panelDead.SetActive(true);
    }   


    public void PlayerHit(string value){
        if (damageText != null) {
            var damage = Instantiate(damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", value);
        }
    }

    void PlayerInteraction() {
        Debug.DrawRay(hand.position, dir * 0.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(hand.position, dir, 0.1f, interaction);

        if (hit == true) {
            objInteraction = hit.collider.gameObject;
            alertInteraction.SetActive(true);
        
        } else {
            objInteraction = null;
            alertInteraction.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Power":
                Destroy(other.gameObject);
                break;
            
            case "Coin":
                other.gameObject.SendMessage("Colection", SendMessageOptions.DontRequireReceiver);
                break;
        }
    }

    public void checkpoint()
    {
         
        ReloadLevel();
        rewarded = true;        
    }

}