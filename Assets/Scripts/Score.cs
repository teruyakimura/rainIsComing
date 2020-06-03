using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    private float timer;
    public float score;
    public bool enable_score;
    private float increase;
    
    void Start()
    {
        ScoreText.text = "Score: 0";
        enable_score = true;
        score = 0;
        increase = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;

        if (enable_score == true)
        {
 
            

            //We only need to update the text if the score changed.
            ScoreText.text = "Score: " + (int)score;
            score += increase * Time.deltaTime*1000;


        }
    }
}
