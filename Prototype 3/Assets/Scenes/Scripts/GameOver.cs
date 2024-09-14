using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text finaScoreText;

    private void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finaScoreText.text = "Your Total Money $" + finalScore;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Prototype 3");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
