using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private _GameController _GameController;

    [Header("Image of the Chest")]
    public Sprite[] imgObject;
    private SpriteRenderer spriteRenderer;
    private bool open;

    [Header("Loots")]
    public GameObject[] Coins;

    void Start() {
        _GameController = FindObjectOfType(typeof(_GameController)) as _GameController;        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interaction() {

        if(open == false) {
            open = true;
            spriteRenderer.sprite = imgObject[1];
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine("LootCoins");
        }
    }

    IEnumerator LootCoins() {
        int quantityCoins = Random.Range(4,8);
        for (int l = 0; l < quantityCoins; l++) {
            int rand = 0;
            int idLoot = 0;
            rand = Random.Range(0,100);

            if(rand >= 75) {
                idLoot = 1;
            }
            
            GameObject lootTemp = Instantiate(Coins[idLoot], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30), 95));
            yield return new WaitForSeconds(0.1f);
        }
    }
}