using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Advance to the next Scene in the order (Game or Level0)
        Time.timeScale = 1;
}

    public void QuitGame(){
    	Application.Quit();
    }
}
