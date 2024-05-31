using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDUI : UI
{
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private TMP_Text _castleHPText;

    [SerializeField] private TMP_Text _scoreText;

    public override void Initialize()
    {
        GameManager.Instance.UpdateUI(GameManager.Instance.CastleHP, GameManager.Instance.Score, GameManager.Instance.ElapsedTime, false);
    }

    public override void UpdateUI(int CastleHP, int Score, float ElapsedTime, bool wonTheGame)
    {
        _timerText.text = "Timer: " + (int)ElapsedTime;

        _castleHPText.text = "Castle Health: " + CastleHP;

        _scoreText.text = "Score: " + Score;
    }
}
