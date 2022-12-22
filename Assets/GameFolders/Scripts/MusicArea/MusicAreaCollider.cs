using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicAreaCollider : AreasMainSettings
{
    [SerializeField] GameObject instrument;
    [SerializeField] GameObject customers;
    [SerializeField] BoxCollider areaCollider;

    MusicArea _musicArea;
    MusicPlayerController _musicPlayer;
    AreaCanvasController _areaCanvasController;
    BoxCollider _boxCollider;

    public bool IsUnclock => isUnlock;
    public int NeededMoney
    {
        get => NeededMoneyData;
        set => NeededMoneyData = value;
    }


    private void Awake()
    {
        _musicArea = GetComponentInChildren<MusicArea>();
        _musicPlayer = GetComponentInChildren<MusicPlayerController>();
        _areaCanvasController = GetComponentInChildren<AreaCanvasController>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        if (UnlockData == 2)
        {
            _musicArea.gameObject.SetActive(true);
            isUnlock = true;
            neededMoney = 0;
            instrument.transform.localScale = Vector3.one;
            _musicPlayer.transform.localScale = Vector3.one;
            _boxCollider.size = new Vector3(10, 1, 10);
            _areaCanvasController.gameObject.SetActive(false);
            ColliderSettings();
            customers.SetActive(true);
        }
        else
        {
            _musicArea.gameObject.SetActive(false);
            isUnlock = false;
        }
    }

    public void UnlockArea()
    {
        isUnlock = true;
        UnlockData = 2;
        _musicArea.OpenArea();
        OpenEffect(instrument);
        OpenEffect(_musicPlayer.gameObject);
        _areaCanvasController.OpenAreaEffect();
        _boxCollider.size = new Vector3(10, 1, 10);
        customers.SetActive(true);
    }

    private void OpenEffect(GameObject obj)
    {
        obj.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void ColliderSettings()
    {
        if (areaCollider.isTrigger == true) areaCollider.isTrigger = false;
    }
}
