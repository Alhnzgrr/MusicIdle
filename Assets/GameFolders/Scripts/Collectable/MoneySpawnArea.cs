using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public class MoneySpawnArea : MonoBehaviour
{
    [Header("Editor Music Box Creator Settings")]
    [SerializeField] int moneyAmount;
    [SerializeField] MoneyController moneyPrefab;
    [SerializeField] List<MoneyController> moneys;
    [SerializeField] Transform moneyParent;

    [Header("Run Time Music Box Settings")]
    [SerializeField] float moneyCreateTime;
    [SerializeField] float jumpPoseZ;
    [SerializeField] float xOffset;
    [SerializeField] int maxProductAmount;
    

    private float currentTime;

    //[Button("Create Music Boxes")]
    //private void CreateMusicBoxes()
    //{
    //    int count = transform.childCount;
    //    for (int i = 0; i < count; i++)
    //    {
    //        DestroyImmediate(transform.GetChild(0).gameObject);
    //    }

    //    moneys.Clear();

    //    for (int i = 0; i < moneyAmount; i++)
    //    {
    //        MoneyController money = (MoneyController)UnityEditor.PrefabUtility.InstantiatePrefab(moneyPrefab, transform);

    //        moneys.Add(money);

    //        money.transform.localPosition = Vector3.zero;
    //        money.transform.rotation = Quaternion.identity;
    //    }
    //}

    private void Start()
    {
        currentTime = moneyCreateTime;
        MoneysPreLoad();
    }

    private void Update()
    {
        if (moneyCreateTime > 0) moneyCreateTime -= Time.deltaTime;
        if (moneyCreateTime <= 0)
        {
            CreateMoney();
            moneyCreateTime = currentTime;
        }
    }

    private void CreateMoney()
    {
        if (GetProductAmount() >= maxProductAmount) return;
        MoneyController musicBox = GetLastUnVisibilityMusicBox();
        musicBox.JumpToProductPosition();
        musicBox.SetVisibility(true);
    }


    private void MoneysPreLoad()
    {
        int x = 0;
        float y = 0;

        for (int i = 0; i < moneys.Count; i++)
        {
            x = i % 4;

            if (i % 4 == 0 && i != 0)
            {
                y += 0.25f;
            }

            moneys[i].SetVisibility(false);
            moneys[i].SetPositions(Vector3.zero, new Vector3(x - xOffset, y, -jumpPoseZ));
        }
    }

    private MoneyController GetLastUnVisibilityMusicBox()
    {
        foreach (MoneyController money in moneys)
        {
            if (!money.Visibility)
            {
                return money;
            }
        }

        return null;
    }

    private MoneyController GetLastVisibilityMoney()
    {
        for (int i = 0; i < moneys.Count; i++)
        {
            if (!moneys[i].Visibility)
            {
                if (i != 0)
                {
                    return moneys[i - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        return null;
    }

    public void CloseMoneys(int amount, out List<Vector3> givenBoxesPositions)
    {
        List<Vector3> boxesPositions = new List<Vector3>();

        for (int i = 0; i < amount; i++)
        {
            if (GetLastVisibilityMoney() != null)
            {
                boxesPositions.Add(GetLastVisibilityMoney().transform.position);
                GetLastVisibilityMoney().SetVisibility(false);
            }
        }

        givenBoxesPositions = boxesPositions;
    }

    private int GetProductAmount()
    {
        for (int i = 0; i < moneys.Count; i++)
        {
            if (!moneys[i].Visibility)
            {
                return i;
            }
        }
        return moneys.Count;
    }


}
