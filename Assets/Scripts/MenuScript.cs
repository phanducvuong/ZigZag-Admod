using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public Text ScoreText;
    public Text BestScoreText;
    public Text Score;
    public Image BackgroundMenu;

    private int tempScore;
    PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        data = SaveSystem.LoadPlayer();
    }

    void Update()
    {
        ScoreText.text = Score.text;
        tempScore = int.Parse(ScoreText.text);

        if (tempScore > data.bestScore)
            BestScoreText.text = tempScore.ToString();
        else
            BestScoreText.text = data.bestScore.ToString();
    }
}
