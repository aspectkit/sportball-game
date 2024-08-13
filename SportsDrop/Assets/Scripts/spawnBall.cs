using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class spawnBall : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public GameObject previousBall;
    public int scoreNum;
    private ballController ball_script;

    public GameObject marblePrefab;
    public GameObject golfPrefab;
    public GameObject billiardPrefab;
    public GameObject tennisPrefab;

    public GameObject cricketPrefab;

    List<GameObject> ballList = new List<GameObject>();
    void Start()
    {
        ball_script = previousBall.GetComponent<ballController>();
        ballList.Add(marblePrefab);
        ballList.Add(golfPrefab);
        ballList.Add(billiardPrefab);
        ballList.Add(tennisPrefab);
        ballList.Add(cricketPrefab);
        scoreNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball_script.ballDropped){
            int randomBallIndex = Random.Range(0, ballList.Count);
            GameObject b = Instantiate(ballList[randomBallIndex]) as GameObject;
            ball_script = b.GetComponent<ballController>();
            scoreNum++;
            scoreText.text = "Score: " + scoreNum.ToString();
        }
    }
}
