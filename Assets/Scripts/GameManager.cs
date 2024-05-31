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
        UpdateUI(CastleHP, Score, ElapsedTime, false);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) {
            case GameState.Start:
                break;
            case GameState.Ongoing:
                FeetSpawner.Instance.StartWaves();
                UIManager.Show<HUDUI>(true);
                break;
            case GameState.Victory:
                UIManager.Show<EndMenuUI>(true);
                EndGame(true);
                break;
            case GameState.Lose:
                UIManager.Show<EndMenuUI>(true);
                EndGame(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void UpdateScore(int newScore)
    {
        Score += newScore;

        UpdateUI(CastleHP, Score, ElapsedTime, false);
    }

    public void UpdateCastleHP(int damage)
    {
        CastleHP -= damage;

        if(CastleHP <= 0)
        {
            UpdateGameState(GameState.Lose);
        }

        UpdateUI(CastleHP, Score, ElapsedTime, false);
    }

    public void UpdateUI(int CastleHP, int Score, float ElapsedTime, bool wonTheGame)
    {
        UIManager.GetUI<HUDUI>().UpdateUI(CastleHP, Score, ElapsedTime, wonTheGame);
    }

    private void EndGame(bool wonTheGame)
    {
        UIManager.GetUI<EndMenuUI>().UpdateUI(CastleHP, Score, ElapsedTime, wonTheGame);
    }

    public enum GameState
    {
        Start,
        Ongoing,
        Victory,
        Lose
    }
}
