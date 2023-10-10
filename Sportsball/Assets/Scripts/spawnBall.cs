using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBall : MonoBehaviour
{
    // Start is called before the first frame update
    // gets the initial starting ball to later know when its dropped
    public GameObject previousBall;
    // gets the ballController script to grab the ballDropped variable from ballController
    private ballController ball_script;
    // gets the prefab to be the next ball to spawn and for the user to drop. will change this so its a random prefab 
    public GameObject ballPrefab;
    
    // start function starts when game starts
    // ball script is the inital starting ball
    // starts an endless loop in waitBall
    void Start()
    {
        ball_script = previousBall.GetComponent<ballController>();
        StartCoroutine(waitBall());
    }

    // Update is called once per frame
    void Update()
    {
            
        
    }

    // an endless loop that stalls for 2 seconds and then calls the createBall function
    // we stall for 2 seconds here so that a new ball is not immediately spawned right when the previous ball drops
    // if there is no delay, the ball that is falling will push the new ball without user input 
    IEnumerator waitBall()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            createBall();
        }
        
    }

    // function that creates a new ball once the previous one has been dropped
    // check if the ball has dropped
    // if it has, create a new ball based on the prefab (will make the prefab random later)
    // make the initial position the same for all new balls (i'm not sure if this is necessary because the prefab already has the correct position)
    // since the previous ball keeps changing we also need to point the script to the new previous ball otherwise the ballDropped variable will always be true after the initial ball
    // that'll cause an infinite amount of balls to spawn 
    private void createBall()
    {
        if (ball_script.ballDropped)
        {
            GameObject b = Instantiate(ballPrefab) as GameObject;
            b.transform.position = new Vector2(0, 3.5f);
            ball_script = b.GetComponent<ballController>();
        }
        
    }
}
