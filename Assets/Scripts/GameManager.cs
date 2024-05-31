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

    [SerializeField] private GameObject castlePart1;
    [SerializeField] private GameObject castlePart2;
    [SerializeField] private GameObject castlePart3;
    [SerializeField] private GameObject castlePart4;

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

        switch(CastleHP)
        {
            case 0:
                castlePart4.SetActive(false);
                UpdateGameState(GameState.Lose);
                break;
            case 2:
                castlePart3.SetActive(false);
                break;
            case 4:
                castlePart2.SetActive(false);
                break;
            case 6:
                castlePart1.SetActive(false);
                break;
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
