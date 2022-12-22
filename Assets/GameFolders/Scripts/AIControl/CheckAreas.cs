using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckAreas : MonoSingleton<CheckAreas>
{
    [SerializeField] internal List<MusicAreaMusicBoxes> musicAreas = new List<MusicAreaMusicBoxes>();
    [SerializeField] internal List<MusicBoxArea> musicBoxAreas = new List<MusicBoxArea>();
    [SerializeField] internal List<AIController> helpers = new List<AIController>();
    [SerializeField] internal List<BeverageVendingUnlockArea> beverages = new List<BeverageVendingUnlockArea>();

    private void Awake()
    {
        Singleton();
    }

    public MusicAreaMusicBoxes GetLeastMusicArea()
    {
        MusicAreaMusicBoxes musicAreaMusicBoxes = musicAreas.OrderBy(m => m.GetMusicBoxesAmount()).FirstOrDefault(m => m.GetComponent<MusicAreaCollider>().IsUnclock);

        return musicAreaMusicBoxes;
    }

    public MusicBoxArea GetHighMusicBoxArea()
    {
        MusicBoxArea musicArea = musicBoxAreas.OrderByDescending(m => m.GetProductAmount()).FirstOrDefault();

        return musicArea;
    }

    public AIController GetLockedHelper()
    {
        foreach (AIController helper in helpers)
        {
            if (!helper.isUnlock)
            {
                return helper;
            }
        }
        return null;
    }

    public BeverageVendingUnlockArea GetBeverageVending()
    {
        foreach (BeverageVendingUnlockArea machine in beverages)
        {
           if(machine.beverageType == UpgradeCanvasController.Instance._adType) 
            {
                return machine;
            }
        }
        return null;
    }

    public void HelperCapacityUpgrade()
    {
        foreach (AIController helper in helpers)
        {
            helper.GetComponent<MusicBoxController>().defaultCollectBound++;
        }
    }

    public void SpeedUpgrade()
    {
        foreach (AIController helper in helpers)
        {
            helper.SpeedUpgrade();
        }
    }
}
