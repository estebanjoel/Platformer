using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text scoreLabel;

    private void Start() {
        ResetHUD();
    }

    public void ResetHUD(){

        scoreLabel=GameObject.Find("scoreLabel").GetComponent<Text>();
        scoreLabel.text="Score: "+ GameManager.instance.score;
    
    }
}
