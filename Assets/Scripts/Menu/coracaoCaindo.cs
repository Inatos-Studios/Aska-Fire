using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class coracaoCaindo : MonoBehaviour
{
public float speed = 1.5f;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void cairCore()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.Rotate (new Vector3(0,0,speed));


    }

    }