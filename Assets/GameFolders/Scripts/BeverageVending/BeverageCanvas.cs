using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BeverageCanvas : MonoBehaviour
{
    [Header("Area Canvas Settings")]
    [SerializeField] Image lockImage;
    [SerializeField] Image filledImage;
    [SerializeField] TextMeshProUGUI neededWatchText;

    BeverageVendingUnlockArea _beverageVendingUnlockArea;

    private void Awake()
    {
        _beverageVendingUnlockArea = GetComponentInParent<BeverageVendingUnlockArea>();
    }

    private void Start()
    {
        neededWatchText.text = $"{_beverageVendingUnlockArea.AdsWatchIndex}/3";
        filledImage.DOFillAmount((float)_beverageVendingUnlockArea.AdsWatchIndex / 3, 1f);

    }

    public void UpdateText()
    {
        //filledImage.fillAmount = Mathf.InverseLerp(0, 0, _beverageVendingUnlockArea.AdsWatchIndex);
        filledImage.DOFillAmount((float)_beverageVendingUnlockArea.AdsWatchIndex / 3 , 1f);
        neededWatchText.text = $"{_beverageVendingUnlockArea.AdsWatchIndex}/3";
    }
}
