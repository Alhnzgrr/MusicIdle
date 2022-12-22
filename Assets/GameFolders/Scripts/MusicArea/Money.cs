using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    private bool isCollected;

    private void OnEnable()
    {
        isCollected = false;
    }

    public void Jump(Transform player)
    {
        if (isCollected) return;
        isCollected = true;
        transform.DOJump(player.position, 3, 1, 0.25f).OnComplete(() =>
        {
            transform.localPosition = Vector3.zero;
            GameManager.Instance.Money++;
            UIController.Instance.MoneyTextUpdate();
            isCollected = false;
            gameObject.SetActive(false);

        });
    }
}
