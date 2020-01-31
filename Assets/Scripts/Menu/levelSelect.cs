using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelect : MonoBehaviour
{

    public GameObject painelCave;
    public GameObject painelForest;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump")){
            painelCave.SetActive(false);
            painelForest.SetActive(true);
        }
    }
}
