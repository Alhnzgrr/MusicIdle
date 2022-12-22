using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreasMainSettings : MonoBehaviour
{
    [SerializeField] protected string areaName;
    [SerializeField] protected string areaNeededMoneyName;
    [SerializeField] internal int neededMoney;
    [SerializeField] protected bool isUnlock;

    protected int UnlockData 
    {
        get => PlayerPrefs.GetInt(areaName , 1);  // If Value is 1 => Unlock False!!
        set => PlayerPrefs.SetInt(areaName , value); // If Value is 2 => Unlock True!!
    }

    protected int NeededMoneyData
    {
        get => PlayerPrefs.GetInt(areaNeededMoneyName, neededMoney);
        set => PlayerPrefs.SetInt(areaNeededMoneyName, value); 
    }
}
