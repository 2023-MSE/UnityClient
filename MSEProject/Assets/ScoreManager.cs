using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreint;

    private int sum = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : ";
        scoreint.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scoreUpdate(int score,bool check)
    {
        if (check == true)
        {

            sum += score;
            Debug.Log(score + ", " + sum);
            //scoreint.text = sum.ToString();
            scoreint.text = sum.ToString();
            check = !check;
        }
    }
}
