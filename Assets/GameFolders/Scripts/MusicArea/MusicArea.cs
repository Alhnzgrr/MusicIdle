using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MusicArea : MonoBehaviour
{
    [SerializeField] private List<Money> moneys = new List<Money>();
    [SerializeField] private float moneySetTime;
    [SerializeField] private Vector3 moneySpawnPosition;

    MusicAreaMusicBoxes _musicAreaMusicBoxes;
    EventData _eventData;

    private Vector3 defaultPosition;
    private float moneySpawnTime;
    private int moneysIndex = 0;
    private int horizontalIndex = 0;
    private int musicBoxesIndex = 0;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
        _musicAreaMusicBoxes = GetComponentInParent<MusicAreaMusicBoxes>();
        moneys.AddRange(GetComponentsInChildren<Money>());
    }

    private void Start()
    {
        moneySpawnTime = moneySetTime;
        defaultPosition = moneySpawnPosition;
        foreach (Money money in moneys)
        {
            money.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_musicAreaMusicBoxes.GetMusicBoxesAmount() <= 0) return;
        if (moneySetTime > 0)
        {
            moneySetTime -= Time.deltaTime;
        }
        else
        {
            if (moneysIndex == moneys.Count || _musicAreaMusicBoxes.GetMusicBoxesAmount() <= 0) return;
            SpawnMoney();
            moneySetTime = moneySpawnTime;
        }
    }

    private void SpawnMoney()
    {
        if (horizontalIndex == 4)
        {
            horizontalIndex = 0;
            moneySpawnPosition.y += 0.05f;
            moneySpawnPosition.x = 0.25f;
        }
        Money money = moneys[moneysIndex];
        money.transform.localPosition = Vector3.zero;
        money.gameObject.SetActive(true);
        money.transform.DOLocalJump(moneySpawnPosition, 1.5f, 1, 0.25f);
        moneysIndex++;
        horizontalIndex++;
        moneySpawnPosition.x -= 0.3f;
        musicBoxesIndex++;
        if (musicBoxesIndex >= 1)
        {
            _musicAreaMusicBoxes.CloseBoxes(1);
            musicBoxesIndex = 0;
        }

    }

    public void OpenArea()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void ResetMoney(Transform player)
    {
        StartCoroutine(ResetMoneyCoroutine(player));
        horizontalIndex = 0;
        moneySpawnPosition = defaultPosition;
    }

    IEnumerator ResetMoneyCoroutine(Transform player)
    {
        for (int i = moneysIndex - 1; i >= 0; i--)
        {
            moneys[i].Jump(player);

            yield return new WaitForSeconds(0.025f);
        }

        moneysIndex = 0;
    }
}
