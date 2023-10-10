using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    // Start is called before the first frame update
    // moveAllowed allows user to move ball when the screen is touched
    bool moveAllowed;
    // prevents user to move ball when ball is dropped and used to determine if a new ball can be spawned in spawnBall script
    public bool ballDropped;
    // gets the rigidbody of the current ball so we can manipulate the gravity of the ball when the user releases from the screen
    private Rigidbody2D _rigidBody;

    // gets the rigidbody of the ball
    // ball has not dropped yet
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        ballDropped = false;

    }

    // Update is called once per frame
    void Update()
    {
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
                if (transform.position.y == 3.5)
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
                _rigidBody.gravityScale = 1;
                // the ball has now dropped (give time for the ball to drop before moving on)
                StartCoroutine(waiter());
                
                
            }
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        ballDropped = true;
    }

    
}
