using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    // Start is called before the first frame update
    bool moveAllowed;
    bool ballDropped;
    Collider2D col;
    private Rigidbody2D _rigidBody;
    public GameObject ballPrefab;
    void Start()
    {
        col = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        ballDropped = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);


            if (touch.phase == TouchPhase.Began)
            {
                if (!ballDropped)
                {
                    moveAllowed = true;
                    transform.position = new Vector2(touchPosition.x, transform.position.y);
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, transform.position.y);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false;
                _rigidBody.gravityScale = 1;
                ballDropped = true;
                
                StartCoroutine(waitBall());
                
                
                
            }
        }
    }

    IEnumerator waitBall()
    {
        yield return new WaitForSeconds(2);
        createBall();
    }

    private void createBall()
    {
        GameObject b = Instantiate(ballPrefab) as GameObject;
        b.transform.position = new Vector2(0, 3.5f);
    }
}
