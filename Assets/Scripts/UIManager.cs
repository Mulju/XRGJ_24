using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager s_instance;

    [SerializeField] private UI _startingUI;

    [SerializeField] private UI[] _uis;

    private UI _currentUI;

    private readonly Stack<UI> _history = new Stack<UI>();

    private void Awake() => s_instance = this;

    public static T GetUI<T>() where T : UI
    {
        for (int i = 0; i < s_instance._uis.Length; i++)
        {
            if (s_instance._uis[i] is T tUI)
            {
                return tUI;
            }
        }

        return null;
    }

    public static void Show<T>(bool remeber = true) where T : UI
    {
        for (int i = 0; i < s_instance._uis.Length; i++)
        {
            if (s_instance._uis[i] is T)
            {
                if (s_instance._currentUI != null)
                {
                    if (remeber)
                    {
                        s_instance._history.Push(s_instance._currentUI);
                    }

                    s_instance._currentUI.Hide();
                }

                s_instance._uis[i].Show();

                s_instance._currentUI = s_instance._uis[i];
            }
        }
    }

    public static void Show(UI ui, bool remeber = true)
    {
        if (s_instance._currentUI != null)
        {
            if (remeber)
            {
                s_instance._history.Push(s_instance._currentUI);
            }
            
            s_instance._currentUI.Hide();
        }
        
        ui.Show();

        s_instance._currentUI = ui;
    }

    public static void ShowLast()
    {
        if (s_instance._history.Count != 0)
        {
            Show(s_instance._history.Pop(), false);
        }
    }

    private void Start()
    {
        for (int i = 0; 1 < _uis.Length; i++)
        {
            _uis[i].Initialize();

            _uis[i].Hide();
        }

        if (_startingUI != null)
        {
            Show(_startingUI, true);
        }
    }
}
