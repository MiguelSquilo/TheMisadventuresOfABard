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
    public int vidaBoss;
    private GameObject boss;

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
        boss = GetComponent<BoardManager>().bossLife;
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
        /*
        if (level == 10)
        {
            Debug.Log("BossSpawn");
            vidaBoss.SetActive(true);
        }
        */
        
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
            SceneManager.LoadScene(2); //Advance to the next Scene in the order (Game or Level0)
            level = 0;
        }
        if (vidaBoss == 0 && enemyCount == 0)
        {
        	level = 0;
            SceneManager.LoadScene(3);
            vidaBoss = 1;
        }else{
        	vidaBoss = boss.GetComponent<EnemyHealthManager>().currentHealth - 2;
        }
        
    }
}