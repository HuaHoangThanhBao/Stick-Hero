using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandle : MonoBehaviour {

    GameObject panelPoint;
    GameObject panelEnd;
    GameObject startPanel;

    Text scoreInGame_txt;
    Text scoreEndGame_txt;
    Text highScore_txt;
    Text guide_txt;

    PlayerControl player;

    PlayerProgress playerProgress;

    float timeToShow;
    int temp = 0;
    int score = 0;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        panelPoint = GameObject.Find("Panel Point");
        panelEnd = GameObject.Find("Panel End");
        startPanel = GameObject.Find("Start Panel");
        scoreInGame_txt = GameObject.Find("Score In Game Text").GetComponent<Text>();
        scoreEndGame_txt = GameObject.Find("Score End Game Text").GetComponent<Text>();
        highScore_txt = GameObject.Find("High Score Text").GetComponent<Text>();
        guide_txt = GameObject.Find("Guide").GetComponent<Text>();
    }

    private void Start()
    {
        LoadScore();
    }

    private void Update()
    {
        SetActiveCanvas();
        ScoreControl();
    }

    void ScoreControl()
    {
        if (player.variables.seenPoint && player.variables.getNumber > 0)
        {
            if(temp == 0)
            {
                score++;

                scoreInGame_txt.text = score.ToString();

                temp++;
            }
        }

        if (score > 2) guide_txt.gameObject.SetActive(false);

        if(player.variables.endGame)
        {
            scoreEndGame_txt.text = score.ToString();

            SubmitScore(score);

            highScore_txt.text = getHighScore().ToString();
        }

        if (player.variables.walk) temp = 0;
    }

    int getHighScore()
    {
        return playerProgress.highScore;
    }

    void LoadScore()
    {
        playerProgress = new PlayerProgress();
        if (PlayerPrefs.HasKey("highScore"))
        {
            playerProgress.highScore = PlayerPrefs.GetInt("highScore");
        }
    }

    void SubmitScore(int newScore)
    {
        if(playerProgress.highScore < newScore)
        {
            playerProgress.highScore = newScore;
            PlayerPrefs.SetInt("highScore", newScore);
        }
    }

    void SetActiveCanvas()
    {
        if (player.variables.gameBegin)
        {
            panelPoint.SetActive(true);
            panelEnd.SetActive(false);
            startPanel.SetActive(false);
        }
        else
        {
            panelPoint.SetActive(false);
            panelEnd.SetActive(false);
            startPanel.SetActive(true);
        }

        if (player.variables.endGame)
        {
            timeToShow += Time.deltaTime;
            if (timeToShow > 0.2f)
            {
                panelPoint.SetActive(false);
                panelEnd.SetActive(true);
                startPanel.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        player.variables.gameBegin = true;
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StickHero");
    }
}
