﻿using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Completed.BoardManager boardScript;
    private int level = 1;
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
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
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