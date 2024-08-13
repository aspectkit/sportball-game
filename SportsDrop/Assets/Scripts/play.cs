using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class play : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    [SerializeField] private AudioSource buttonSound;

    public void goToGame(){

        buttonSound.Play();
        StartCoroutine(loadGame());
    }

    IEnumerator loadGame(){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("game");

    }

    public void goToCredits(){
        buttonSound.Play();
        StartCoroutine(loadCredits());
    }

    IEnumerator loadCredits(){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("credits");
    }
}
