using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mergeBall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nextBallPrefab;
    private bool isColliding;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
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
            //Debug.Log(gameObject.GetInstanceID());
            Destroy(collision.gameObject);
            Destroy(gameObject);
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
            {
                //Instantiate(golfPrefab, transform.position, Quaternion.identity);
                GameObject b = (GameObject)Instantiate(nextBallPrefab);
                b.transform.position = transform.position;
                b.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            
        } else if (collision.gameObject.tag == gameObject.tag && gameObject.tag == "beachball")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
