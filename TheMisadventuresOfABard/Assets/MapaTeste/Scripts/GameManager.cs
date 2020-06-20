using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Completed.BoardManager boardScript;
    public int level = 1;
    public int enemyCount = 0;
    public int currentHealth = 50;
    public int maxHealth = 50;

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
    }


    private void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();       
    }

    // Update is called once per frame
    void Update()
    {

    }
}