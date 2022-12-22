using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeCanvasController : MonoSingleton<UpgradeCanvasController>
{
    [Header(" ----- ADD HELPER -----")]
    [SerializeField] Button addHelplerButton;
    [SerializeField] TextMeshProUGUI addHelperText;
    [SerializeField] TextMeshProUGUI addHelperNeededMoneyText;
    [SerializeField] Button addHelperAdButton;

    [Header(" ----- SPEED -----")]
    [SerializeField] Button speedButton;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI speedNeededMoneyText;
    [SerializeField] Button speedAdButton;

    [Header(" ----- CAPACÝTY -----")]
    [SerializeField] Button capacityButton;
    [SerializeField] TextMeshProUGUI capacityText;
    [SerializeField] TextMeshProUGUI capacityNeededMoneyText;
    [SerializeField] Button capacityAdButton;

    internal AdType _adType;
    EventData _eventData;
    internal bool canAdWatch = true;

    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
        addHelplerButton.onClick.AddListener(AddHelperButtonClick);
        speedButton.onClick.AddListener(SpeedButtonClick);
        capacityButton.onClick.AddListener(CapacityButtonClick);
        addHelperAdButton.onClick.AddListener(AddHelperAdButtonClick);
        speedAdButton.onClick.AddListener(SpeedAdButtonClick);
        capacityAdButton.onClick.AddListener(CapacityAdButtonClick);
    }

    private void OnEnable()
    {
        _eventData.UpgradePanelOpen += OpenPanelEvent;
        _eventData.UpgradePanelClose += ClosePanelEvent;
    }

    private void Start()
    {
        CheckMoneyForUpgrade();
        transform.localScale = Vector3.zero;
    }

    private void OnDisable()
    {
        _eventData.UpgradePanelOpen -= OpenPanelEvent;
        _eventData.UpgradePanelClose -= ClosePanelEvent;
    }

    #region UPGRADE BUTTON CLÝCK

    private void AddHelperButtonClick()
    {
        if (GameManager.Instance.Money >= UpgradeManager.Instance.AddHelperNeededMoney && UpgradeManager.Instance.CanUpgradeAddHelper())
        {
            UpgradeManager.Instance.AddHelperUpgrade();
            ScaleEffect(addHelplerButton);
        }
    }

    private void SpeedButtonClick()
    {
        if (GameManager.Instance.Money >= UpgradeManager.Instance.SpeedNeededMoney)
        {
            UpgradeManager.Instance.SpeedUpgrade();
            ScaleEffect(speedButton);
        }
    }

    private void CapacityButtonClick()
    {
        if (GameManager.Instance.Money >= UpgradeManager.Instance.CapacityNeededMoney)
        {
            UpgradeManager.Instance.CapacityUpgrade();
            ScaleEffect(capacityButton);
        }
    }

    private void ScaleEffect(Button button)
    {
        button.transform.DOScale(Vector3.one * 1.1f, 0.15f).SetEase(Ease.Flash).OnComplete(() =>
        {
            button.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.Flash);
        });
        CheckMoneyForUpgrade();
    }

    public void CheckMoneyForUpgrade()
    {
        Color whiteColor = new Color(1, 1, 1, 1);
        Color colorTransparent = new Color(1, 1, 1, 0.5f);
        Color redColor = new Color(1, 0, 0, 0.5f);
        Color greenColor = new Color(0, 1, 0, 1);

        UIController.Instance.MoneyTextUpdate();
        addHelperText.text = $"Add Helper";
        addHelperNeededMoneyText.text = $"${UpgradeManager.Instance.AddHelperNeededMoney}";

        speedText.text = $"Speed";
        speedNeededMoneyText.text = $"${UpgradeManager.Instance.SpeedNeededMoney}";

        capacityText.text = $"Capacity";
        capacityNeededMoneyText.text = $"${UpgradeManager.Instance.CapacityNeededMoney}";

        if (UpgradeManager.Instance.AddHelperNeededMoney > GameManager.Instance.Money)
        {
            addHelperAdButton.GetComponent<Image>().color = colorTransparent;
            addHelperText.GetComponent<TextMeshProUGUI>().color = colorTransparent;
            addHelperNeededMoneyText.GetComponent<TextMeshProUGUI>().color = redColor;
        }
        else
        {
            addHelperAdButton.GetComponent<Image>().color = whiteColor;
            addHelperText.GetComponent<TextMeshProUGUI>().color = whiteColor;
            addHelperNeededMoneyText.GetComponent<TextMeshProUGUI>().color = greenColor;
        }

        if (UpgradeManager.Instance.SpeedNeededMoney > GameManager.Instance.Money)
        {
            speedAdButton.GetComponent<Image>().color = colorTransparent;
            speedText.GetComponent<TextMeshProUGUI>().color = colorTransparent;
            speedNeededMoneyText.GetComponent<TextMeshProUGUI>().color = redColor;
        }
        else
        {
            speedAdButton.GetComponent<Image>().color = whiteColor;
            speedText.GetComponent<TextMeshProUGUI>().color = whiteColor;
            speedNeededMoneyText.GetComponent<TextMeshProUGUI>().color = greenColor;
        }

        if (UpgradeManager.Instance.CapacityNeededMoney > GameManager.Instance.Money)
        {
            capacityAdButton.GetComponent<Image>().color = colorTransparent;
            capacityText.GetComponent<TextMeshProUGUI>().color = colorTransparent;
            capacityNeededMoneyText.GetComponent<TextMeshProUGUI>().color = redColor;
        }
        else
        {
            capacityAdButton.GetComponent<Image>().color = whiteColor;
            capacityText.GetComponent<TextMeshProUGUI>().color = whiteColor;
            capacityNeededMoneyText.GetComponent<TextMeshProUGUI>().color = greenColor;
        }

        if (AdManager.Instance.RewardedAd.IsLoaded() && canAdWatch)
        {
            addHelperAdButton.GetComponent<Image>().color = whiteColor;
        }
        else
        {
            addHelperAdButton.GetComponent<Image>().color = colorTransparent;
        }

        if (AdManager.Instance.RewardedAd.IsLoaded() && canAdWatch)
        {
            speedAdButton.GetComponent<Image>().color = whiteColor;
        }
        else
        {
            speedAdButton.GetComponent<Image>().color = colorTransparent;
        }

        if (AdManager.Instance.RewardedAd.IsLoaded() && canAdWatch)
        {
            capacityAdButton.GetComponent<Image>().color = whiteColor;
        }
        else
        {
            capacityAdButton.GetComponent<Image>().color = colorTransparent;
        }
    }

    #endregion

    #region AD UPGRADE BUTTON CLÝCK

    private void AddHelperAdButtonClick()
    {
        if (!canAdWatch || !AdManager.Instance.RewardedAd.IsLoaded()) return;
        _adType = AdType.AddHelper;
        canAdWatch = false;
        AdManager.Instance.RewardedAd.Show();
        CheckMoneyForUpgrade();
        StartCoroutine(AdWatchBool());
    }
    private void SpeedAdButtonClick()
    {
        if (!canAdWatch || !AdManager.Instance.RewardedAd.IsLoaded()) return;
        _adType = AdType.Speed;
        canAdWatch = false;
        AdManager.Instance.RewardedAd.Show();
        CheckMoneyForUpgrade();
        StartCoroutine(AdWatchBool());
    }
    private void CapacityAdButtonClick()
    {
        if (!canAdWatch || !AdManager.Instance.RewardedAd.IsLoaded()) return;
        _adType = AdType.Capacity;
        canAdWatch = false;
        AdManager.Instance.RewardedAd.Show();
        CheckMoneyForUpgrade();
        StartCoroutine(AdWatchBool());
    }
    IEnumerator AdWatchBool()
    {
        yield return new WaitForSeconds(5);
        canAdWatch = true;
    }

    #endregion

    #region AD UPGRADE

    public void AddHelperAd()
    {
        UpgradeManager.Instance.AddHelperAd();
    }

    public void SpeedAd()
    {
        UpgradeManager.Instance.SpeedAd();
    }

    public void CapacityAd()
    {
        UpgradeManager.Instance.CapacityAd();
    }

    #endregion

    #region  AD GÝVE REWARD
    public void GiveReward()
    {
        switch (_adType)
        {
            case AdType.AddHelper:
                AddHelperAd();
                break;
            case AdType.Speed:
                SpeedAd();
                break;
            case AdType.Capacity:
                CapacityAd();
                break;
            case AdType.Machine1:
                CheckAreas.Instance.GetBeverageVending().WatchAd();
                StartCoroutine(AdWatchBool());
                break;
            case AdType.Machine2:
                CheckAreas.Instance.GetBeverageVending().WatchAd();
                StartCoroutine(AdWatchBool());
                break;
            case AdType.Machine3:
                CheckAreas.Instance.GetBeverageVending().WatchAd();
                StartCoroutine(AdWatchBool());
                break;
            default:
                break;
        }
    }
    #endregion

    #region EVENT LISTENER

    private void OpenPanelEvent()
    {
        transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InFlash);
    }

    private void ClosePanelEvent()
    {
        transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InFlash);
    }

    #endregion
}
