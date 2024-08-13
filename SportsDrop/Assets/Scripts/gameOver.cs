using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameOver : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    [SerializeField] private AudioSource buttonSound;


    public void goToHome(){
        buttonSound.Play();
        StartCoroutine(loadHome());
    }

    IEnumerator loadHome(){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("home");

    }
}
