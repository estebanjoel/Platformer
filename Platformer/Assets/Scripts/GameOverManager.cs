using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //access to the text element that shows the score value
    public Text scoreValue;
    //access the high score value
    public Text highScoreValue;
    // Start is called before the first frame update
    void Start()
    {
        //set the text propertie of the score value field
        scoreValue.text=GameManager.instance.score.ToString();
        //set the text propertie of the high score value field
        highScoreValue.text=GameManager.instance.highScore.ToString();
    }

    public void RestartGame(){
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("Level1");
    }

    public void ReturnToMainMenu(){
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("MainMenu");
    }
}
