using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Chamar a biblioteca de gerenciamento de cenas.

public class loadGame : MonoBehaviour{

    private float myTimeout; // Criando variável do tempo limite.
    void Start(){
        myTimeout = Time.time+0.3f; // Definindo variável de tempo limite para 2
    }

    void Update(){
    if(Time.time > myTimeout){ // se, o tempo atual, for menor que minha variável, ou seja, se passou os 2 segundos.
        SceneManager.LoadScene("INTRO"); //Carregar cena Fase 1.
        }
    }
}