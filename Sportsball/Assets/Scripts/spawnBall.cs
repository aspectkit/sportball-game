using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class spawnBall : MonoBehaviour
{
    // Start is called before the first frame update
    // gets the initial starting ball to later know when its dropped
    public GameObject previousBall;
    // gets the ballController script to grab the ballDropped variable from ballController
    private ballController ball_script;
    // gets the prefab to be the next ball to spawn and for the user to drop. prefab is random
    public GameObject marblePrefab;
    public GameObject golfPrefab;
    public GameObject billiardPrefab;
    public GameObject tennisPrefab;
    public GameObject cricketPrefab;
    //private float timer = 3.0f;
    // list of all the prefabs to choose a random one
    List<GameObject> ballList = new List<GameObject>();
    
    

    // start function starts when game starts
    // ball script is the inital ballController script attached to the starting ball
    // add all prefabs to the ball list
    void Start()
    {
        ball_script = previousBall.GetComponent<ballController>();
        ballList.Add(marblePrefab);
        ballList.Add(golfPrefab);
        ballList.Add(billiardPrefab);
        ballList.Add(tennisPrefab);
        ballList.Add(cricketPrefab);


    }

    // Update is called once per frame
    // call createBall function
    void Update()
    {
        if (ball_script.ballDropped)
        {
            Debug.Log("Ball dropped!");
            int randomBallIndex = Random.Range(0, ballList.Count);
            GameObject b = Instantiate(ballList[randomBallIndex]) as GameObject;
            //b.transform.position = new Vector2(0, 3.5f);
            ball_script = b.GetComponent<ballController>();
        }

        




    }

    // function that creates a new ball once the previous one has been dropped
    // check if the ball has dropped
    // if it has, create a new ball based on the prefab 
    // prefab is a random one from the list
    // since the previous ball keeps changing we also need to point the script to the new previous ball otherwise the ballDropped variable will always be true after the initial ball
    // that'll cause an infinite amount of balls to spawn 




}
