using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ballController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool moveAllowed;
    public bool ballDropped;
    private Rigidbody2D rb;
    private bool isColliding;
    public GameObject nextBallPrefab;
    private GameObject b;
    private bool merged = false;
    public GameObject lineMarkerPrefab;
    private GameObject lineMarker;
    private TextMeshProUGUI scoreText;
    public int scoreNum;
    private spawnBall text_script;
    private bool scoreAdded;
    private GameObject endLine;
    private ParticleSystem explo;
    private Animator transition;
    private AudioSource combineSound;

    void Start()
    {
        scoreAdded = false;
        transition = GameObject.Find("CircleWipe").GetComponent<Animator>();
        explo = GameObject.Find("combine").GetComponent<ParticleSystem>();
        text_script = GameObject.Find("gameController").GetComponent<spawnBall>();
        scoreText = GameObject.Find("score").GetComponent<TextMeshProUGUI>();
        combineSound = GameObject.Find("combineSound").GetComponent<AudioSource>();
        endLine = GameObject.Find("endline");
        rb = GetComponent<Rigidbody2D>();
        if (merged){
            ballDropped = true;
        } else {
            ballDropped = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
            if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began){
                if (transform.position.y == 4){
                    moveAllowed = true;
                    transform.position = new Vector2(touchPosition.x, transform.position.y);
                    lineMarker = Instantiate(lineMarkerPrefab) as GameObject;
                    lineMarker.transform.position = new Vector2(touchPosition.x, -0.5f);
                }
            }

            if (touch.phase == TouchPhase.Moved){
                if (moveAllowed){
                    transform.position = new Vector2(touchPosition.x, transform.position.y);
                    lineMarker.transform.position = new Vector2(touchPosition.x, -0.5f);
                }
            }

            if (touch.phase == TouchPhase.Ended){
                moveAllowed = false;
                rb.gravityScale = 2f;
                GameObject[] lines = GameObject.FindGameObjectsWithTag("linehelper");
                foreach (GameObject line in lines){
                    Destroy(line);
                }
                StartCoroutine(waiter());
            }
        }
    }


    IEnumerator Blink(float blinktime){
        while (Time.time < blinktime) {
            endLine.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            endLine.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator waiter(){
        yield return new WaitForSeconds(0.5f);
        ballDropped = true;
        if (transform.position.y >= 2 && ballDropped){
            StartCoroutine(Blink(3.0f));
            yield return new WaitForSeconds(1.0f);
            if (transform.position.y >= 2 && ballDropped){
                PlayerPrefs.SetInt("userScore", text_script.scoreNum);
                if (PlayerPrefs.GetInt("userScore", 0) > PlayerPrefs.GetInt("highScore", 0)){
                    PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("userScore", 0));
                }
                transition.SetTrigger("Start");
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("gameover");
            } 
        }
    }

    void createExplo(){
        explo.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        
        if (isColliding){
            return;
        } 
        isColliding = true;

        if (collision.gameObject.tag == gameObject.tag && gameObject.tag != "beachball"){
            if (!scoreAdded){
                scoreNum = text_script.scoreNum + 1;
                text_script.scoreNum = scoreNum;
                scoreText.text = "Score: " + scoreNum.ToString();
                scoreAdded = true;
            }
            
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID()){
                combineSound.Play();
                b = Instantiate(nextBallPrefab) as GameObject;
                b.GetComponent<ballController>().merged = true;
                b.GetComponent<ballController>().ballDropped = true;
                b.transform.position = new Vector2(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y + 0.2f);
                b.GetComponent<Rigidbody2D>().gravityScale = 2f;

                if (b.transform.position.y >= 2 && ballDropped){
                    StartCoroutine(waiter());
                }

                gameObject.GetComponent<ballController>().ballDropped = true;
                collision.gameObject.GetComponent<ballController>().ballDropped = true;
                explo.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                createExplo();
                Destroy(collision.gameObject);
                Destroy(gameObject);
                
            }
        } else if (collision.gameObject.tag == gameObject.tag && gameObject.tag == "beachball"){
                if (!scoreAdded){
                scoreNum = text_script.scoreNum + 1;
                text_script.scoreNum = scoreNum;
                scoreText.text = "Score: " + scoreNum.ToString();
                scoreAdded = true;
            }
                combineSound.Play();
                gameObject.GetComponent<ballController>().ballDropped = true;
                collision.gameObject.GetComponent<ballController>().ballDropped = true;
                explo.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                createExplo();
                Destroy(collision.gameObject);
                Destroy(gameObject);
                
        }
    }
    private void OnCollisionStay2D(Collision2D collision){
    
      
        
        if (collision.gameObject.tag == gameObject.tag && gameObject.tag != "beachball"){
            Debug.Log("Entered Stay");
            if (!scoreAdded){
                scoreNum = text_script.scoreNum + 1;
                text_script.scoreNum = scoreNum;
                scoreText.text = "Score: " + scoreNum.ToString();
                scoreAdded = true;
            }
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID()){
                combineSound.Play();
                b = Instantiate(nextBallPrefab) as GameObject;
                b.GetComponent<ballController>().merged = true;
                b.GetComponent<ballController>().ballDropped = true;
                b.transform.position = new Vector2(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y + 0.2f);
                b.GetComponent<Rigidbody2D>().gravityScale = 2f;

                if (b.transform.position.y >= 2 && ballDropped){
                    StartCoroutine(waiter());
                }

                gameObject.GetComponent<ballController>().ballDropped = true;
                collision.gameObject.GetComponent<ballController>().ballDropped = true;
                explo.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                createExplo();
                Destroy(collision.gameObject);
                Destroy(gameObject);
                
                
            }
        } else if (collision.gameObject.tag == gameObject.tag && gameObject.tag == "beachball"){
                if (!scoreAdded){
                scoreNum = text_script.scoreNum + 1;
                text_script.scoreNum = scoreNum;
                scoreText.text = "Score: " + scoreNum.ToString();
                scoreAdded = true;
            }
                combineSound.Play();
                gameObject.GetComponent<ballController>().ballDropped = true;
                collision.gameObject.GetComponent<ballController>().ballDropped = true;
                explo.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                createExplo();
                Destroy(collision.gameObject);
                Destroy(gameObject);
                
        }
    }
}
