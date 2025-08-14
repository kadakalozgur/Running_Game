using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public void Setup()
    {

        gameObject.SetActive(true);

    }

    public void restartButton()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("SampleScene");

    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
