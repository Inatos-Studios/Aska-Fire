using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public void LoadSceneOnClick(int sceneIndex)
    {
        SceneManager.LoadScene(5);

    }
}
