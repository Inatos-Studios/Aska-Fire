using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private string cenaAtual;
    public GameObject painel;
    public Button jump;
    public Button interact;
    public Button Attack;
    

    private void Start() 
    {
        cenaAtual = SceneManager.GetActiveScene ().name;
    }

    public void pausar()
    {
        painel.SetActive(true);
        Time.timeScale = 0f;
        jump.interactable = false;
        interact.interactable = false;
        Attack.interactable = false;
    }


    public void reiniciar()
    {
        SceneManager.LoadScene (cenaAtual);
        Time.timeScale = 1f;
    }

    public void LoadSceneOnClick(int sceneIndex) // Menu
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void despausar()
    {
        painel.SetActive(false);
        Time.timeScale = 1f;
        jump.interactable = true;
        interact.interactable = true;
        Attack.interactable = true;
        
    }









}
