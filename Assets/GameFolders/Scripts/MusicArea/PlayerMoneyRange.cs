using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyRange : MonoBehaviour
{
    MusicArea _musicArea;

    private bool isPlayerRange = false;

    private void Awake()
    {
        _musicArea = GetComponentInParent<MusicArea>();
    }

    public void PlayerTriggered(Transform player)
    {
        _musicArea.ResetMoney(player);
    }
}
