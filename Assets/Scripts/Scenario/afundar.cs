using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afundar : MonoBehaviour
{

    private Rigidbody2D rb; //Variável do RigidBody2D
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Pegando componente do objeto e definindo á variável.
    }

    void OnCollisionEnter2D(Collision2D col) // Função " Ao Colidir 2D"
    {
        if (col.gameObject.name == "Player") // Se, a colisão, for com o gameObject chamado Player.
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Definir o RigidBody TYPE para Dynamic ( Está originalmente como static).
        }    

        if (col.gameObject.name == "Tilemap") // Se, a colisão, for com o gameObject chamado Tilemap
        {
            Destroy(gameObject);  //Destroy o gameObject ( no caso, a qual o script está ).
        }
    
    }
}


