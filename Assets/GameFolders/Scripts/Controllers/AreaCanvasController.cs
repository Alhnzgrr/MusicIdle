using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class AreaCanvasController : MonoBehaviour
{
    [Header("Area Canvas Settings")]
    [SerializeField] Image dolarImage;
    [SerializeField] Image filledImage;
    [SerializeField] Image lockImage;
    [SerializeField] Image blackBackGround;
    [SerializeField] Image whiteBackGround;
    [SerializeField] TextMeshProUGUI neededMoneyText;

    MusicAreaCollider _musicArea;
    float defaultNeededMoney;

    private void Awake()
    {
        _musicArea = GetComponentInParent<MusicAreaCollider>();
    }

    private void Start()
    {
        defaultNeededMoney = _musicArea.neededMoney;
        neededMoneyText.text = $"{_musicArea.NeededMoney}";
        filledImage.fillAmount = Mathf.InverseLerp(defaultNeededMoney, 0, _musicArea.NeededMoney);
    }

    private void Update()
    {
        filledImage.fillAmount = Mathf.InverseLerp(defaultNeededMoney, 0, _musicArea.NeededMoney);
    }

    public void OpenAreaEffect()
    {
        transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Flash).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void TextEffect()
    {
        neededMoneyText.text = $"{_musicArea.NeededMoney}";
        neededMoneyText.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.InFlash).OnComplete(() =>
        {
            neededMoneyText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InFlash);
        });
    }

    public void UnlockData()
    {
        dolarImage.gameObject.SetActive(false);
        filledImage.gameObject.SetActive(false);
        neededMoneyText.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);
        blackBackGround.gameObject.SetActive(false);
        whiteBackGround.gameObject.SetActive(false);
    }
}
