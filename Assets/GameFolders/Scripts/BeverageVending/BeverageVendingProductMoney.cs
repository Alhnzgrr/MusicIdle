using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using DG.Tweening;

public class BeverageVendingProductMoney : MonoBehaviour
{
    [Header("Editor Money Creator Settings")]
    [SerializeField] int moneyAmount;
    [SerializeField] Money moneyPrefab;
    [SerializeField] List<Money> moneys;
    [SerializeField] Transform moneyParent;

    [Header("InGame Money Settings")]
    [SerializeField] private float moneySetTime;
    [SerializeField] private Vector3 moneySpawnPosition;

    BeverageVendingUnlockArea beverangeParent;

    private Vector3 defaultPosition;
    private float moneySpawnTime;
    private int moneysIndex = 0;

    //[Button("Create Money")]
    //private void CreateMusicBoxes()
    //{
    //    int count = moneyParent.childCount;
    //    for (int i = 0; i < count; i++)
    //    {
    //        DestroyImmediate(moneyParent.GetChild(0).gameObject);
    //    }

    //    moneys.Clear();

    //    for (int i = 0; i < moneyAmount; i++)
    //    {
    //        Money musicBox = (Money)UnityEditor.PrefabUtility.InstantiatePrefab(moneyPrefab, moneyParent.transform);

    //        moneys.Add(musicBox);

    //        musicBox.transform.localPosition = Vector3.zero;
    //        musicBox.transform.rotation = Quaternion.identity;
    //    }
    //}

    private void Awake()
    {
        beverangeParent = GetComponentInParent<BeverageVendingUnlockArea>();
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
        if (moneySetTime > 0)
        {
            moneySetTime -= Time.deltaTime;
        }
        else
        {
            if (moneysIndex == moneys.Count) return;
            SpawnMoney();
            moneySetTime = moneySpawnTime;
        }
    }

    private void SpawnMoney()
    {
        Money money = moneys[moneysIndex];
        money.transform.localPosition = Vector3.zero;
        money.gameObject.SetActive(true);
        money.transform.DOLocalJump(moneySpawnPosition + Vector3.up * moneysIndex * + 0.15f, 1.5f, 1, 0.25f);
        moneysIndex++;
    }
    public void ResetMoney(Transform player)
    {
        StartCoroutine(ResetMoneyCoroutine(player));
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
