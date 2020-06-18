using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public GameObject pauseButton;

	public void pause(){
		Time.timeScale = 0;
	}

	public void resume(){
		Time.timeScale = 1;
	}

	public void quitGame(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //Advance to the next Scene in the order (Game or Level0)
    }
}
