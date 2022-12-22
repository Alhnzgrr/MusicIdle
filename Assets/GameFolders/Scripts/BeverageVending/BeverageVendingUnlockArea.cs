using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeverageVendingUnlockArea : MonoBehaviour
{
    [SerializeField] string beverageVendingName;
    [SerializeField] string beverageAdsData;
    [SerializeField] internal AdType beverageType;
    [SerializeField] bool isMachineOpen = false;

    BeverageVendingProductMoney _beverageVendingProductMoney;
    BeverageCanvas _beverageCanvas;
    private float moneyResetTime = 0.25f;
    private float unlockTime = 1;


    public int BeverageVendingData
    {
        get => PlayerPrefs.GetInt(beverageVendingName, 1);
        set => PlayerPrefs.SetInt(beverageVendingName, value);
    }

    public int AdsWatchIndex
    {
        get => PlayerPrefs.GetInt(beverageAdsData);
        set => PlayerPrefs.SetInt(beverageAdsData, value);
    }

    private void Awake()
    {
        _beverageVendingProductMoney = GetComponentInChildren<BeverageVendingProductMoney>();
        _beverageCanvas = GetComponentInChildren<BeverageCanvas>();
    }

    private void Start()
    {
        if (BeverageVendingData == 1)
        {
            _beverageVendingProductMoney.gameObject.SetActive(false);
            isMachineOpen = false;
        }
        else
        {
            _beverageCanvas.gameObject.SetActive(false);
            _beverageVendingProductMoney.gameObject.SetActive(true);
            isMachineOpen = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isMachineOpen)
            {
                if (!AdManager.Instance.RewardedAd.IsLoaded() || !UpgradeCanvasController.Instance.canAdWatch) return;
                if (unlockTime > 0)
                {
                    unlockTime -= Time.deltaTime;
                }
                else
                {
                    UpgradeCanvasController.Instance._adType = beverageType;
                    AdManager.Instance.RewardedAd.Show();
                    unlockTime = 1;
                }
            }
            else
            {
                if (moneyResetTime > 0)
                {
                    moneyResetTime -= Time.deltaTime;
                }
                else
                {
                    _beverageVendingProductMoney.ResetMoney(other.transform);
                    moneyResetTime = 0.25f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            unlockTime = 1;
        }
    }

    public void WatchAd()
    {
        if (isMachineOpen) return;
        AdsWatchIndex++;
        _beverageCanvas.UpdateText();
        if (AdsWatchIndex == 3)
        {
            UnlockArea();
            BeverageVendingData++;
        }
    }

    private void UnlockArea()
    {
        _beverageVendingProductMoney.transform.localScale = Vector3.zero;
        _beverageVendingProductMoney.gameObject.SetActive(true);
        _beverageVendingProductMoney.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce);
        _beverageCanvas.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Flash).OnComplete(() => _beverageCanvas.gameObject.SetActive(false));
        isMachineOpen = true;
    }
}
