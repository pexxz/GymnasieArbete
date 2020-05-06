using UnityEngine;
using TMPro;

public class ScoreManenger : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int myScore;

    void Start()
    {
        //Set myScore var to 0 at start
        myScore = 0;
    }
    void Update()
    {
        //Uppdates Score displayed on Screen
        scoreText.text = myScore.ToString();
    }
}
