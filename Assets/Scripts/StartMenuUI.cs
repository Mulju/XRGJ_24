using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : UI
{
    [SerializeField] private Button _startButton;

    [SerializeField] private Button _exitButton;
    public override void Initialize()
    {
        _startButton.onClick.AddListener(() => GameManager.Instance.UpdateGameState(GameManager.GameState.Ongoing));
        _exitButton.onClick.AddListener(() => Application.Quit());
    }

    public override void UpdateUI(int CastleHP, int Score, float ElapsedTime, bool w)
    {
        return;
    }
}
