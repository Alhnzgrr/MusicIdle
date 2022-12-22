using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoSingleton<UIController>
{
    [Header("Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Buttons")]
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button tryAgainButton;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Joystick")]
    [SerializeField] DynamicJoystick _dynamicJoystick;

    private EventData _eventData;

    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
    }
    private void Start()
    {
        MoneyTextUpdate();
    }

    public void MoneyTextUpdate()
    {
        moneyText.text = $"Money {GameManager.Instance.Money} ";
    }

    #region Joystick

    public float GetHorizontal()
    {
        return _dynamicJoystick.Horizontal;
    }

    public float GetVertical()
    {
        return _dynamicJoystick.Vertical;
    }

    #endregion
}
