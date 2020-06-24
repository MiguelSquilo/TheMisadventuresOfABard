using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Completed.BoardManager boardScript;
    public int level = 1;
    public int enemyCount = 0;
    public int currentHealth = 6;
    public int maxHealth = 6;
    public GameObject vidaBoss;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        //tirar so esta aqui para se testar no Asset Mapa mas quando for para testar o Asset Menu commentar o InitGame()
        //InitGame();
    }

    

    void InitGame()
    {
        boardScript.SetupScene(level);
        enemyCount = GetComponent<BoardManager>().count -1;
        if (level == 1)
        {
            currentHealth = 6;
        }


    }


    private void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
        //Para o boss ter vida mas nao aparece fica inactive(entra no log mas o setActive nao funciona)
        if (level == 2)
        {
            Debug.Log("entrofdfsdfsd");
            vidaBoss.SetActive(false);
        }
        
    }

    public void meterVidaCheia()
    {
        currentHealth = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //Advance to the next Scene in the order (Game or Level0)
            level = 0;
        }
        
    }
}