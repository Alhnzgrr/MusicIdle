using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockArea : MonoBehaviour
{
    [SerializeField] private float stayTimeMusicArea;

    private float stayTimeDefaultValue;
    private bool isInMusicRangePlayer;

    private void Start()
    {
        stayTimeDefaultValue = stayTimeMusicArea;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            MusicAreaCollider area = other.GetComponent<MusicAreaCollider>();
            if (area.IsUnclock) return;

            if (stayTimeMusicArea > 0) stayTimeMusicArea -= Time.deltaTime;
            if (stayTimeMusicArea <= 0)
            {
                if (GameManager.Instance.Money > 0 && area.NeededMoney > 0)
                {
                    if (!isInMusicRangePlayer)
                    {
                        isInMusicRangePlayer = true;
                        StartCoroutine(OpenMusicArea(area));
                    }
                }
                else if (area.NeededMoney <= 0)
                {
                    area.UnlockArea();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            isInMusicRangePlayer = false;
            stayTimeMusicArea = stayTimeDefaultValue;
        }
    }

    IEnumerator OpenMusicArea(MusicAreaCollider area)
    {
        while (isInMusicRangePlayer)
        {
            GameManager.Instance.Money -= 1;
            area.NeededMoney -= 1;
            area.GetComponentInChildren<AreaCanvasController>().TextEffect();
            UIController.Instance.MoneyTextUpdate();
            if (area.NeededMoney <= 0) isInMusicRangePlayer = false;
            yield return new WaitForSeconds(0.0025f);

        }
    }
}
