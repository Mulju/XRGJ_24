using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuUI : UI
{
    [SerializeField] private TMP_Text _victoryText;

    [SerializeField] private TMP_Text _loseText;

    [SerializeField] private Button _restartButton;

    [SerializeField] private Button _exitButton;

    public override void Initialize()
    {
        _victoryText.gameObject.SetActive(false);
        _loseText.gameObject.SetActive(false);
        _restartButton.onClick.AddListener(() => GameManager.Instance.UpdateGameState(GameManager.GameState.Ongoing));
        _exitButton.onClick.AddListener(() => Application.Quit());
    }

    public override void UpdateUI(int CastleHP, int Score, float ElapsedTime, bool wonTheGame)
    {
        if (wonTheGame)
        {
            _victoryText.gameObject.SetActive(true);
        }
        else
        {
            _loseText.gameObject.SetActive(true);
        }
    }
}
