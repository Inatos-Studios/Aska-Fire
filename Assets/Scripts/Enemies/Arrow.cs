using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Vector3 direction;
    private Rigidbody2D rb2d;
    private Transform player;

    [Header("Movement")]    
    public float speed;
    public Vector3 directionArrow;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Transform> ();
    }
    
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        directionArrow = (player.position - transform.position).normalized;
    }

    public void Initialize(Vector3 _direction) {
        direction = _direction;
    }

    void Update() {
        transform.position += directionArrow * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Colisão com o Player");
            Arrived();
        
        } else if (other.tag == "Scenario") {
            Debug.Log("Colisão com Cenário");
            Arrived();
        }
    }

    void Arrived() {
        Destroy(gameObject);
    }
}