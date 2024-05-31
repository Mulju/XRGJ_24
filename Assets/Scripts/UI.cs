using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    public abstract void Initialize();

    public abstract void UpdateUI(int CastleHP, int Score, float ElapsedTime, bool wonTheGame);

    public virtual void Hide() => gameObject.SetActive(false);

    public virtual void Show() => gameObject.SetActive(true);
}
