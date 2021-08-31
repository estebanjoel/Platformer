using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //score of the player
    public int score = 0;
    //high score
    public int highScore = 0;
    //current level
    public int currentLevel = 1;
    //how levels they are
    public int highestLevel = 4;
    //HUD managers
    HUDManager hudManager;
    // static instance of the GM can be accessed from anywhere
    public static GameManager instance;
    //musicAudioSource
    public GameObject musicSource;
    // Start is called before the first frame update
    void Awake() 
    {
        //check that it exists
        if(instance==null){
            //assign it to the current object
            instance=this;
        }
        //make sure that is equal
        else if(instance!=this){
            //Destroy the current game object, we only need one and we already have it
            Destroy(gameObject);
        }
        //don't destroy this object when changing scenes
        DontDestroyOnLoad(gameObject);
        if(hudManager==null){
            hudManager=FindObjectOfType<HUDManager>(); 
        }

        DontDestroyOnLoad(musicSource);
        
    }

    private void Update() {
         if(GameObject.FindGameObjectsWithTag("Audio").Length>1){
            Destroy(GameObject.FindGameObjectsWithTag("Audio")[1]);
        }

        if(SceneManager.GetActiveScene().name=="MainMenu"||SceneManager.GetActiveScene().name=="GameOver"){
            musicSource.GetComponent<AudioSource>().Stop();
        }

        else{
            if(!musicSource.GetComponent<AudioSource>().isPlaying){
                musicSource.GetComponent<AudioSource>().Play();
            }
            
        }
    }

    //increase score
    public void IncreaseScore(int amount){
        score+=amount;
        hudManager.ResetHUD();
        if(score>highScore){
            highScore=score;
        }
    }

    //reset the game
    public void ResetGame(){
        //reset our score
        score=0;   
        //set current level to 1
        currentLevel=1;
        if(hudManager!=null){
            hudManager.ResetHUD();
        }
    }

    //send the player to the next level
    public void IncreaseLevel(){
        //check if there are more levels
        if(currentLevel < highestLevel){
            currentLevel++;
        }
        else{
            currentLevel=1;
        }
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void GameOver(){

        SceneManager.LoadScene("GameOver");
    
    }
}
