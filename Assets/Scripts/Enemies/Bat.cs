using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

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

    [Header("Audios")]

    protected bool isMoving = false;
    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected Transform player;
    protected SpriteRenderer sprite;

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
        if(health > 0)
        Move();
    }

    void FixedUpdate() {
        if (health > 0)

        if (isMoving) {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Mathf.Abs(speed) * Time.deltaTime);
            anim.SetTrigger("Fly");
            alertEnemy.SetActive(true);
        
        } else if (!isMoving) {
            anim.SetTrigger("Idle");
            alertEnemy.SetActive(false);
        }
    }

    void Move() {
        float distance = PlayerDistance ();
        isMoving = (distance <= distanceAttack);

        if (isMoving) {
            if ((player.position.x > transform.position.x && sprite.flipX) || (player.position.x < transform.position.x && !sprite.flipX)) {
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

    void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player" && health > 0) {
            anim.SetTrigger("Attack");
//            SoundManager.instance.PlaySound(fxAttack);
        }
    }

    IEnumerator DamageEffect() {
        anim.SetTrigger("Hurt");
        float actualSpeed = speed;
        speed = speed * -2;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        speed = actualSpeed;
        sprite.color = Color.white;
//        SoundManager.instance.PlaySound(fxHurt);
    }

    void DamageEnemy() {
        health--;
        StartCoroutine(DamageEffect());

        if (health < 1) {
            BatDead();
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
        }
    }

    void BatDead() {
        GetComponent<Collider2D>().enabled = false;        
        anim.SetTrigger("Dead");
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