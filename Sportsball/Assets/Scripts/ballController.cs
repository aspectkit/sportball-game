using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ballController : MonoBehaviour
{
    // Start is called before the first frame update
    // moveAllowed allows user to move ball when the screen is touched
    bool moveAllowed;
    // prevents user to move ball when ball is dropped and used to determine if a new ball can be spawned in spawnBall script
    public bool ballDropped;
    // gets the rigidbody of the current ball so we can manipulate the gravity of the ball when the user releases from the screen
    private Rigidbody2D _rigidBody;
    private bool isColliding;
    public GameObject nextBallPrefab;
    private GameObject b;
    private bool merged = false;
    // gets the rigidbody of the ball
    // ball has not dropped yet
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        if (merged)
        {
            ballDropped = true;
        } else
        {
            ballDropped = false;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
        // checks if user has touched the screen
        if (Input.touchCount > 0)
        {
            // gets properties of the user's first touch
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // when the user initially touches the screen
            if (touch.phase == TouchPhase.Began)
            {
                // checks to make sure the ball hasnt dropped and prevents the user from manipulating it if it has
                if (transform.position.y == 4)
                {
                    // user can move the ball left and right and the ball immediately moves to where the user initially touched the screen
                    moveAllowed = true;
                    transform.position = new Vector2(touchPosition.x, transform.position.y);
                }
            }
            // when user holds their finger on the screen
            if (touch.phase == TouchPhase.Moved)
            {
                // makes sure user hasnt let go of screen 
                if (moveAllowed)
                {
                    // changes position of ball in the x based on where the finger is 
                    transform.position = new Vector2(touchPosition.x, transform.position.y);
                }
            }
            // if users lets go of the screen
            if (touch.phase == TouchPhase.Ended)
            {
                // ball cannot be moved
                moveAllowed = false;
                // gives the ball gravity
                _rigidBody.gravityScale = 2f;
                // the ball has now dropped (give time for the ball to drop before moving on)
                StartCoroutine(waiter());
                
                
            }
        }
        if (transform.position.y >= 2 && ballDropped)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
        ballDropped = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
        {
            return;
        }
        isColliding = true;

        if (collision.gameObject.tag == gameObject.tag && gameObject.tag != "beachball")
        {
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
            {
                Debug.Log("here");
                //Instantiate(golfPrefab, transform.position, Quaternion.identity);
                b = Instantiate(nextBallPrefab) as GameObject;
                b.GetComponent<ballController>().merged = true;
                b.GetComponent<ballController>().ballDropped = true;
                b.transform.position = transform.position;
                b.GetComponent<Rigidbody2D>().gravityScale = 2f;
                if (b.transform.position.y >= 2 && b.GetComponent<ballController>().ballDropped)
                {
                    SceneManager.LoadScene("SampleScene");
                }
                gameObject.GetComponent<ballController>().ballDropped = true;
                collision.gameObject.GetComponent<ballController>().ballDropped = true;
                Destroy(collision.gameObject);
                Destroy(gameObject);
            } 
            


        }
        else if (collision.gameObject.tag == gameObject.tag && gameObject.tag == "beachball")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

   


}
