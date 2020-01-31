using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkull : MonoBehaviour {

    protected bool isMoving = false;
    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected Transform player;
    protected SpriteRenderer sprite;
    private Transform king;    

    [Header("Health")]
    public int health;

    [Header("Movement")]
    public float speed;
    public float distanceAttack;

    [Header("Attack")]
    public Transform attackCheck;
    public float radiusAttack;    
    public LayerMask layerPlayer;
    
    [Header("Damage, Death and Alert")]
    public GameObject damageText; //Declaração para chamada do dano nos inimigos
    public GameObject alertEnemy;

    [Header("Loots")]
    public GameObject[] Coins;

    [Header("Audio")]
    public AudioClip fxAttack;
    public AudioClip fxHurt;
    public AudioClip fxDeath;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        sprite = GetComponent<SpriteRenderer> ();
        player = GameObject.Find("Player").GetComponent<Transform> ();
    }

    void Update() {
        if(health > 0) {
            Move();
        }
    }

    void FixedUpdate() {
        if (health > 0)

        if (isMoving) {
            rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
            alertEnemy.SetActive(true);
        
        } else if (!isMoving) {
            rb2d.velocity = new Vector2 (0f, rb2d.velocity.y);
            alertEnemy.SetActive(false);
        }

        SetAnimations();
    }

    void Move() {
        float distance = PlayerDistance ();
        isMoving = (distance <= distanceAttack);

        if (isMoving) {
            if ((player.position.x > transform.position.x && sprite.flipX) || 
                (player.position.x < transform.position.x && !sprite.flipX)) {
                Flip();
            }
        }
    }

    public float PlayerDistance() {
        return Vector2.Distance(player.position, transform.position);
    }

    protected void Flip() {
        sprite.flipX = !sprite.flipX;
        attackCheck.localPosition = new Vector2(-attackCheck.localPosition.x, attackCheck.localPosition.y);
        speed *= -1;
    }

    void SetAnimations() {
        anim.SetBool ("Run", rb2d.velocity.x != 0f);
        anim.SetBool ("Idle", rb2d.velocity.x == 0f);
        anim.SetBool ("Dead", health <= 0);
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player" && health > 0) {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetTrigger("Attack");
 
        } else if (other.tag != "Player") {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    IEnumerator DamageEffect() {
        anim.SetTrigger("Hurt");
        float actualSpeed = speed;
        speed = speed * -1;
        sprite.color = Color.red;
        rb2d.AddForce (new Vector2(50f, 100f));
        yield return new WaitForSeconds(0.1f);
        speed = actualSpeed;
        sprite.color = Color.white;
//        SoundManager.instance.PlaySound(fxHurt);
    }

    void DamageEnemy() {
        health--;
        StartCoroutine(DamageEffect());

        if (health <1) {
            SwordskullDead();
            StartCoroutine("LootCoins");            
        }
    }

    void OnDrawGizmosSelected() { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }

    void EnemyAttack() {
        Collider2D[] playerAttack = Physics2D.OverlapCircleAll (attackCheck.position, radiusAttack, layerPlayer);
        for (int i = 0; i < playerAttack.Length; i++) {
            playerAttack [i].SendMessage ("DamagePlayer", SendMessageOptions.DontRequireReceiver);
//            SoundManager.instance.PlaySound(fxAttack);
        }
    }
    
    void SwordskullDead() {
        anim.SetTrigger("Dead");
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        alertEnemy.SetActive(false);
//        SoundManager.instance.PlaySound(fxDeath);
    }

    public void EnemyHit(string value){
        if (damageText != null) {
            var damage = Instantiate(damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", value);
        }
    }

    IEnumerator LootCoins() {
        int quantityCoins = Random.Range(1,4);
        for (int l = 0; l < quantityCoins; l++) {
            int rand = 0;
            int idLoot = 0;
            rand = Random.Range(0,100);

            if(rand >= 30) {
                idLoot = 1;
            }

            if(rand >= 85) {
                idLoot = 2;
            }
            
            GameObject lootTemp = Instantiate(Coins[idLoot], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30), 95));
            yield return new WaitForSeconds(0.1f);
        }
    }    
}