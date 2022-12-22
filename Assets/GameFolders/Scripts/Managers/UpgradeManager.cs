using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    #region Add Helper

    public int AddHelperNeededMoney
    {
        get => PlayerPrefs.GetInt("AddHelperNeededMoney", 1000);
        set => PlayerPrefs.SetInt("AddHelperNeededMoney", value);
    }

    public int AddHelperResultMoney
    {
        get => PlayerPrefs.GetInt("AddHelperResultMoney");
        set => PlayerPrefs.SetInt("AddHelperResultMoney", value);
    }

    public int AddHelperMultipler
    {
        get => PlayerPrefs.GetInt("AddHelperResultMoney", 1000);
        set => PlayerPrefs.SetInt("AddHelperResultMoney", value);
    }

    public int AddHelperValue
    {
        get => PlayerPrefs.GetInt("AddHelperValue", 0);
        set => PlayerPrefs.SetInt("AddHelperValue", value);
    }

    #endregion

    #region Speed

    public int SpeedNeededMoney
    {
        get => PlayerPrefs.GetInt("SpeedNeededMoney", 1000);
        set => PlayerPrefs.SetInt("SpeedNeededMoney", value);
    }

    public int SpeedResultMoney
    {
        get => PlayerPrefs.GetInt("SpeedResultMoney");
        set => PlayerPrefs.SetInt("SpeedResultMoney", value);
    }

    public int SpeedMultipler
    {
        get => PlayerPrefs.GetInt("SpeedMultipler", 1000);
        set => PlayerPrefs.SetInt("SpeedMultipler", value);
    }

    public float SpeedValue
    {
        get => PlayerPrefs.GetFloat("SpeedValue");
        set => PlayerPrefs.SetFloat("SpeedValue", value);
    }

    #endregion

    #region Capacity

    public int CapacityNeededMoney
    {
        get => PlayerPrefs.GetInt("CapacityNeededMoney", 1000);
        set => PlayerPrefs.SetInt("CapacityNeededMoney", value);
    }

    public int CapacityResultMoney
    {
        get => PlayerPrefs.GetInt("CapacityResultMoney");
        set => PlayerPrefs.SetInt("CapacityResultMoney", value);
    }

    public int CapacityMultipler
    {
        get => PlayerPrefs.GetInt("CapacityMultipler", 1000);
        set => PlayerPrefs.SetInt("CapacityMultipler", value);
    }

    public int CapacityValue
    {
        get => PlayerPrefs.GetInt("CapacityValue");
        set => PlayerPrefs.SetInt("CapacityValue", value);
    }

    #endregion

    PlayerController _player;
    EventData _eventData;

    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void Start()
    {
        _player = _eventData.Player;
    }

    #region Upgrade Control

    public void AddHelperUpgrade()
    {
        GameManager.Instance.Money -= AddHelperNeededMoney;
        AddHelperResultMoney = AddHelperNeededMoney + AddHelperMultipler;
        AddHelperNeededMoney = AddHelperResultMoney;
        CheckAreas.Instance.GetLockedHelper().UnlockHelper();
        AddHelperValue++;
    }

    public void SpeedUpgrade()
    {
        GameManager.Instance.Money -= SpeedNeededMoney;
        SpeedResultMoney = SpeedNeededMoney + SpeedMultipler;
        SpeedNeededMoney = SpeedResultMoney;
        CheckAreas.Instance.SpeedUpgrade();
        _player.SpeedUpgrade();
        SpeedValue += 0.5f;
    }

    public void CapacityUpgrade()
    {
        GameManager.Instance.Money -= CapacityNeededMoney;
        CapacityResultMoney = CapacityNeededMoney + CapacityMultipler;
        CapacityNeededMoney = CapacityResultMoney;
        CheckAreas.Instance.HelperCapacityUpgrade();
        _player.GetComponent<MusicBoxController>().defaultCollectBound++;
        CapacityValue += 1;
    }

    #endregion

    #region AD UPGRADE

    public void AddHelperAd()
    {
        AddHelperResultMoney = AddHelperNeededMoney + AddHelperMultipler;
        AddHelperNeededMoney = AddHelperResultMoney;
        CheckAreas.Instance.GetLockedHelper().UnlockHelper();
        AddHelperValue++;
    }

    public void SpeedAd()
    {
        SpeedResultMoney = SpeedNeededMoney + SpeedMultipler;
        SpeedNeededMoney = SpeedResultMoney;
        CheckAreas.Instance.SpeedUpgrade();
        _player.SpeedUpgrade();
        SpeedValue += 0.5f;
    }

    public void CapacityAd()
    {
        CapacityResultMoney = CapacityNeededMoney + CapacityMultipler;
        CapacityNeededMoney = CapacityResultMoney;
        CheckAreas.Instance.HelperCapacityUpgrade();
        _player.GetComponent<MusicBoxController>().defaultCollectBound++;
        CapacityValue += 1;
    } 

    #endregion

    public bool CanUpgradeAddHelper()
    {
        return AddHelperValue < 5;
    }

}
