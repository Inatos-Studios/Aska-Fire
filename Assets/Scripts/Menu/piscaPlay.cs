using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Chamar biblioteca de User interface

public class piscaPlay : MonoBehaviour
{
    public float valor; // Criando variável de valor.

    void Start()
    {
        
    }


    void Update()
    {
        valor = Mathf.PingPong(Time.time, 1f); // sabe deus
        Color color = GetComponent<Text>().color; // Pegando a definição de cor do componente texto
        GetComponent<Text>().color = new Vector4(color.r,color.g,color.b, valor); // pegando o componente e mudando a cor do objeto na camada A ( de visibilidade/opacidade)
    }
}
