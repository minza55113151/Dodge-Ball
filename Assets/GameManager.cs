using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float adsRate;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private GameObject textWalkToStart;
    [SerializeField] private GameObject textScore;
    [SerializeField] private GameObject textPressRToRestart;
    [SerializeField] private GameObject textHighScore;
    [SerializeField] private GameObject texttextHighScore;
    [SerializeField] private GameObject textGameOver;

    float score;
    bool isGameStart = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        textWalkToStart.SetActive(true);
        texttextHighScore.SetActive(true);
        textHighScore.SetActive(true);
        textScore.SetActive(false);
        textPressRToRestart.SetActive(false);
        textGameOver.SetActive(false);
        textHighScore.GetComponent<Text>().text = PlayerPrefs.GetInt("highScore").ToString();
    }
    private void Update()
    {
        //if player walk
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !isGameStart)
        {
            //start game
            StartGame();
            isGameStart = true;
        }
        if (isGameStart)
        {
            //update score
            score += Time.deltaTime;
            textScore.GetComponent<Text>().text = ((int)score).ToString();
            HighScore();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    private void HighScore()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        if (highScore < (int)score)
        {
            PlayerPrefs.SetInt("highScore", (int)score);
        }
    }
    private void StartGame()
    {
        textWalkToStart.SetActive(false);
        textHighScore.SetActive(false);
        texttextHighScore.SetActive(false);
        textScore.SetActive(true);
        Instantiate(ballPrefab, new Vector3(0f, 4f, 0f), Quaternion.identity);
    }
    public void GameOver()
    {
        Invoke("SetTimeScale", 1f);
        textPressRToRestart.SetActive(true);
        textGameOver.SetActive(true);
    }
    public void ButtonRestartGame()
    {
        if (adsRate > Random.Range(0f, 1f))
        {
            AdsManager.instance.PlayAd(RestartGame);
            return;
        }
        RestartGame();
    }
    private void RestartGame()
    {
        Input.ResetInputAxes();
        HighScore();
        SceneManager.LoadScene("Scene");
        Time.timeScale = 1f;
    }
    private void SetTimeScale()
    {
        Time.timeScale = 0f;
    }
}
