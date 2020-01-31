using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girarObj : MonoBehaviour
{
    public float speed; // Definir variável de velocidade.

    void Start()
    {
    }

    void Update()
    {
    transform.Rotate (new Vector3(0,0,speed)); // Pega o transform no inspector do objeto, a parte do rotate em especifico, e
                                                // define 0 para x, 0 para y e o valor que você colocar na varável speed, como Z
                                                // para mover o z do objeto no rotation do transform.

    }

    }

