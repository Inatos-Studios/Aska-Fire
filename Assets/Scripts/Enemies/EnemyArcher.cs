using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour {

    private bool facingRight;
    private Animator anim;
    private float timeIdle;
    private float timePatrol;
    private float timeAttack;
    private float playerDistance;
    private bool isPatrolling;
    private bool attack;
//    private bool isDead;

    protected Rigidbody2D rb2d; 
    protected SpriteRenderer sprite; 

    private Transform king;

    [Header("Health")]
    public int health;

    [Header("Movement")]
    public float speed;
    public float durationIdle;
    public float durationPatrol;

    [Header("Attack")]
    public float durationAttack;
    public float attackDistance;
    public GameObject player;
    public GameObject arrowPrefab;
    public Transform spawnArrow;

    [Header("Damage, Death and Alert")]
    public GameObject damageText;
    public GameObject alertEnemy;

    [Header("Loots")]
    public GameObject[] Coins;

    [Header("Audio")]
    public AudioClip fxAttack;
    public AudioClip fxHurt;
    public AudioClip fxDeath;
    
    void Start() {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer> ();
        facingRight = true;
        isPatrolling = false;
        attack = false;
//        isDead = false;
    }

    void Update() {
        ChangeStatus();

        if (health > 0) {
            playerDistance = transform.position.x - player.transform.position.x;
            if (Mathf.Abs(playerDistance) < attackDistance) {
                attack = true;
                isPatrolling = false;
                alertEnemy.SetActive(true);
                Idle();
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                Shoot();
        
            } else {
                ChangeStatus();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Limited") {
            ChangeDirection();
        }
    }

    private void Move() {
        transform.Translate(TakeDirection() * (speed * Time.deltaTime));
        
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private Vector2 TakeDirection() {
        return facingRight ? Vector2.right : Vector2.left;
    }

    private void ChangeDirection() {
        facingRight = !facingRight;
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // MÉTODOS ANIMAÇÃO/AÇÕES

    void Idle() {
        timeIdle += Time.deltaTime;
        if (timeIdle <= durationIdle) {
            anim.SetBool("Run", isPatrolling);
            timePatrol = 0;

        } else {
            isPatrolling = true;
        }
    }

    void Patrol() {
        timePatrol += Time.deltaTime;
        if (timePatrol <= durationPatrol && health > 0) {
            anim.SetBool("Run", isPatrolling);
            Move();
            timeIdle = 0;

        } else {
            isPatrolling = false;
        }
    }

    void Dead() {
            anim.SetBool("Dead", true);
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            Destroy(alertEnemy);
//            SoundManager.instance.PlaySound(fxDeath);
    }

    void Shoot() {
        if (playerDistance < 0 && !facingRight || playerDistance > 0 && facingRight) {
            if (health > 0) {
                ChangeDirection();
            }
        }
        if (attack) {
            timeAttack += Time.deltaTime;
            if (health > 0) {
                if (timeAttack >= durationAttack) {
                    anim.SetTrigger("Attack");
//                    SoundManager.instance.PlaySound(fxAttack);
                    timeAttack = 0;
                }
            }
        }
    }

    void ChangeStatus() {
        if (!attack) {
            if (!isPatrolling) {
                Idle();
            
            } else {
                Patrol();
                alertEnemy.SetActive(false);
            }
        }
    }

    public void ResetAttack() {
        attack = false;
    }

    // MÉTODOS DANO TOMADO/DANO APLICADO

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

        if (health < 1) {
            Dead();
            StartCoroutine("LootCoins");
        }
    }

    public void EnemyHit(string value) {
        if (damageText != null) {
            var damage = Instantiate(damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", value);
        }
    }

    public void Arrow() {
        GameObject cloneArrow = Instantiate(arrowPrefab, spawnArrow.position, spawnArrow.rotation);
        
        if (!facingRight){
            cloneArrow.transform.eulerAngles = new Vector3(180,0,180);
            cloneArrow.GetComponent<Arrow>().Initialize(Vector3.left);
        
        } else {
            cloneArrow.transform.eulerAngles = new Vector3(360,0,360);
            cloneArrow.GetComponent<Arrow>().Initialize(Vector3.right);
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