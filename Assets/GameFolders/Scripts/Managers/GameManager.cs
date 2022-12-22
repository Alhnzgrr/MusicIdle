using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private EventData eventData;
    [SerializeField] int levelCount;
    [SerializeField] int randomLevelLowerLimit;
    [SerializeField] int goldCoefficient;

    GameState _gameState = GameState.Play;

    public GameState GameState
    {
        get => _gameState;
        set => _gameState = value;
    }

    public bool Playability()
    {
        return _gameState == GameState.Play;
    }

    #region Level System
    public int EndlessLevel
    {
        get => PlayerPrefs.GetInt("EndlessLevel", 1);
        set => PlayerPrefs.SetInt("EndlessLevel", value);
    }
    public int Level
    {
        get
        {
            if (PlayerPrefs.GetInt("Level") > levelCount)
            {
                return Random.Range(randomLevelLowerLimit, levelCount);
            }
            else
            {
                return PlayerPrefs.GetInt("Level", 1);
            }
        }
        set => PlayerPrefs.SetInt("Level", EndlessLevel);
    }
    #endregion

    public int Money
    {
        get => PlayerPrefs.GetInt("Money", 10000);
        set => PlayerPrefs.SetInt("Money", value);
    }

    private void Awake()
    {
        Singleton(true);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(Level);
        }
    }

    public void NextLevel()
    {
        _gameState = GameState.Play;
        SceneManager.LoadScene(Level);
    }

    public void MoneyValue()
    {

    }
}
