using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class _GameController : MonoBehaviour {

    [Header("Gold")]
    public int gold;
    public TextMeshProUGUI CoinTxt;


    void Update() 
    {
        CoinTxt.text = gold.ToString("N0");
    }
}

