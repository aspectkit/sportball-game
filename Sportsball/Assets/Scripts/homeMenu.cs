using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class homeMenu : MonoBehaviour
{
    public void goToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void goToHome()
    {
        SceneManager.LoadScene("Welcome");
    }
}
