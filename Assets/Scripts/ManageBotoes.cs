using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageBotoes : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
            PlayerPrefs.SetInt("score", 0);
    }

    public void StartMundoGame()
    {
        SceneManager.LoadScene("Lab1");
    }

    public void FinishMundoGame()
    {
        SceneManager.LoadScene("Lab1_creditos");
    }
}
