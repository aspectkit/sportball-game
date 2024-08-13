using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class displayScores : MonoBehaviour
{
    public TextMeshProUGUI yourscore;
    public TextMeshProUGUI highscore;

    void Start(){
         yourscore.text = "Your Score: " + PlayerPrefs.GetInt("userScore", 0).ToString();
         highscore.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0).ToString();
    }
   
     

    

}
