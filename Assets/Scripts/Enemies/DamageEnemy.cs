using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnemy : MonoBehaviour {

    public Text damage;

    void Start() {
        Destroy(gameObject, 0.5f);        
    }

    public void SetText(string value) {
        damage.text = value;
    }
}