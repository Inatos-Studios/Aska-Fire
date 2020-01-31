    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBoss : MonoBehaviour {

    public enum routine {
        A, B
    }
    
    private Rigidbody2D rb2d;
    private Animator anim;
    private Transform player;
    private EnemyArcher EnemyArcher;
    private Transform enemySkull;
    private SpriteRenderer sprite;

    private float tempTime;
    private float waitTime;

    [Header("Health")]
    public int health;
    
    [Header("Routines")]
    public routine currentRoutine;
    private int idStage;

    [Header("Movement")]
    public float speed;
    private int h;
    private bool isMove;
    public bool Grounded = false;

    [Header("Attack")]
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerPlayer;
    public GameObject swordskullPrefab;
    public Transform teleport;

    [Header("Points")]
    public Transform[] wayPoints;
    private Transform target;
    private bool lookleft;
    public Transform isGrounded;

    [Header("Damage, Death and Alert")]
    public GameObject damageText;
    public GameObject alertEnemy;

    [Header("Spawn Enemies")]
    public Transform skullArcherA;
    public Transform skullArcher;
    public Transform skullWarrior;

    [Header("Audio")]
    public AudioClip fxAttack;
    public AudioClip fxHurt;
    public AudioClip fxDeath;
    public AudioClip fxSummon;
    public AudioClip fxDizzy;
    public AudioClip fxFly;
    public AudioClip fxSliding;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        EnemyArcher = FindObjectOfType(typeof(EnemyArcher)) as EnemyArcher;


// SETUP INICIAL
        currentRoutine = routine.A;
        idStage = 0;
        tempTime = 0;
        waitTime = 3;
    }

    void Update(){
        switch (currentRoutine){
            case routine.A:

                switch (idStage){
                    case 0: // ESPERA 3 SEGUNDOS E DEFINE O DESTINO
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime){
                            idStage += 1;
                            target = wayPoints[1];
                            h = -1;
                            isMove = true;
                        }
                        break;

                    case 1: // MOVE ATÉ O DESTINO
                        if (transform.position.x <= target.position.x){
                            idStage += 1;
                            tempTime = 0;
                            waitTime = 3;
                            h = 0;
                        }
                        break;

                    case 2:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime){
                            idStage += 1;
                            target = wayPoints[0];
                            h = 1;
                        }
                        break;

                    case 3: // FIM DA ROTINA A
                        if (transform.position.x >= target.position.x){
                            h = 0;
                            currentRoutine = routine.B;
                            idStage = 0;
                            tempTime = 0;
                            waitTime = 3;
                        }
                        break;
                }

                break;

            case routine.B:

                switch (idStage){
                    case 0: // ESPERA 3 SEGUNDOS E DEFINE O DESTINO
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime){
                            idStage += 1;
                            target = wayPoints[1];
                            h = -1;
                            isMove = true;
                        }
                        break;

                    case 1: // MOVE ATÉ O DESTINO
                        if (transform.position.x <= target.position.x){
                            idStage += 1;
                            tempTime = 0;
                            waitTime = 3;
                            h = 0;
                        }
                        break;

                    case 2:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime){
                            idStage += 1;
                            target = wayPoints[2];
                            h = 1;
                        }
                        break;

                    case 3: // FIM DA ROTINA A
                        if (transform.position.x >= target.position.x){
                            h = 0;
                            idStage += 1;
                            //rb2d.AddForce(new Vector2(0,320));
                            tempTime = 0;
                        }
                        break;

                    case 4:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime){
                            idStage += 1;
                        }
                        break;

                    case 5:
                        Teleport();
                        SummonArcher();
                        SummonArcherA();
                        SummonMelee();
                        print("Invocar os Esqueletos");
                        tempTime = 0;
                        waitTime = 5;
                        idStage += 1;
                        break;

                    case 6:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime){
                            idStage += 1;
                            isMove = false;
                            //rb2d.AddForce(new Vector2(200, 250));
                            tempTime = 0;
                            waitTime = 1;
                        }
                        break;

                    case 7:
                        tempTime += Time.deltaTime;
                        if (tempTime >= waitTime){
                            
                            if(isGrounded == true) {
                                target = wayPoints[2];
                                h = -1;
                                idStage += 1;
                                isMove = true;
                            }
                        }
                        break;

                    case 8: // FIM DA ROTINA B
                        if (transform.position.x <= target.position.x){
                            int rand = Random.Range(0,100);
                            if(rand < 50) {
                                target = wayPoints[0];
                                h = 1;
                                idStage = 9;
                            
                            } else {
                                target = wayPoints[1];
                                h = -1;
                                idStage = 10;

                            }
                        }
                        break;

                    case 9: // CASO VÁ PARA O PONTO A
                        if (transform.position.x >= target.position.x){
                            idStage = 0;
                            tempTime = 0;
                            waitTime = 3;
                            h = 0;
                            currentRoutine = routine.A;
                        }
                        break;

                    case 10: // CASO VÁ PARA O PONTO B
                        if (transform.position.x <= target.position.x){
                            h = 0;
                        }
                        break;
                }

                break;
        }
       
        if (h > 0 && lookleft == true){
            Flip();
        
        } else if (h < 0 && lookleft == false){
            Flip();
        }

        if (isMove == true){
        rb2d.velocity = new Vector2(h * speed, rb2d.velocity.y);
        }

        anim.SetInteger("h", h);

    }

    void FixedUpdate(){
        Grounded = Physics2D.Linecast(transform.position, isGrounded.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    void Flip(){
        lookleft = !lookleft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    
    IEnumerator DamageEffect(){
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
    
    void DamageEnemy(){
        health--;
        StartCoroutine(DamageEffect());

        if (health < 1) {
            StartCoroutine(KingDead());
        }
    }

    void OnDrawGizmosSelected(){ 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }

    void EnemyAttack(){
        Collider2D[] playerAttack = Physics2D.OverlapCircleAll (attackCheck.position, radiusAttack, layerPlayer);
        for (int i = 0; i < playerAttack.Length; i++) {
            playerAttack [i].SendMessage ("DamagePlayer", SendMessageOptions.DontRequireReceiver);
//            SoundManager.instance.PlaySound(fxAttack);
        }
    }

    IEnumerator KingDead(){
        anim.SetTrigger("Dead");
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        alertEnemy.SetActive(false);
        yield return new WaitForSeconds (0.1f);
//        SoundManager.instance.PlaySound(fxDeath);
    }

    public void EnemyHit(string value){
        if (damageText != null){
            var damage = Instantiate(damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", value);
        }
    }

    public void SummonMelee(){
        GameObject cloneSwordskull = Instantiate(swordskullPrefab, skullWarrior.transform.position, Quaternion.identity);
//        SoundManager.instance.PlaySound(fxSummon);
    }

    public void SummonArcher(){
        EnemyArcher.health = 1;
//        SoundManager.instance.PlaySound(fxSummon);
    }

    public void SummonArcherA(){
        EnemyArcher.health = 1;
//        SoundManager.instance.PlaySound(fxSummon);
    }

    public void Teleport(){
        transform.position = teleport.transform.position;
    }
}