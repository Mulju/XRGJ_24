using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // GameManager memeber attributes
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public int CastleHP = 0;

    public int Score = 0;

    public float ElapsedTime = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if (State == GameState.Ongoing)
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) {
            case GameState.Start:
                break;
            case GameState.Ongoing:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void UpdateScore(int newScore)
    {
        Score += newScore;
    }

    public void UpdateCastleHP(int damage)
    {
        CastleHP -= damage;
    }

    public void UpdateUI(int CastleHP, int Score, float ElapsedTime)
    {
        return;
    }

    public enum GameState
    {
        Start,
        Ongoing,
        Victory,
        Lose
    }


}
