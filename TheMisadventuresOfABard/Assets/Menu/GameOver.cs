using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public void PlayGame(){
    	SceneManager.LoadScene(1); 
        Time.timeScale = 1;
	}

    public void QuitGame(){
    	SceneManager.LoadScene(0);
    }
}
