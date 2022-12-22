using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Linq;

public class MusicBoxArea : MonoBehaviour
{
    [Header("Editor Music Box Creator Settings")]
    [SerializeField] int musicBoxAmount;
    [SerializeField] MusicBox musicBoxPrefab;
    [SerializeField] List<MusicBox> musicBoxes;
    [SerializeField] Transform noteParent;

    [Header("Run Time Music Box Settings")]
    [SerializeField] float musicBoxCreateTime;
    [SerializeField] float jumpPoseZ;
    [SerializeField] float xOffset;
    [SerializeField] int maxProductAmount;
    [SerializeField] internal Transform noteTransform;

    [Header("Save Data")]
    [SerializeField] string musicBoxAreaName;
    [SerializeField] bool isUnlock;

    CarControl _carControl;
    private int boxIndex = 0;
    private float currentTime;


    //[Button("Create Music Boxes")]
    //private void CreateMusicBoxes()
    //{
    //    int count = noteParent.childCount;
    //    for (int i = 0; i < count; i++)
    //    {
    //        DestroyImmediate(noteParent.GetChild(0).gameObject);
    //    }

    //    musicBoxes.Clear();

    //    for (int i = 0; i < musicBoxAmount; i++)
    //    {
    //        MusicBox musicBox = (MusicBox)UnityEditor.PrefabUtility.InstantiatePrefab(musicBoxPrefab, noteParent.transform);

    //        musicBoxes.Add(musicBox);

    //        musicBox.transform.localPosition = Vector3.zero;
    //        musicBox.transform.rotation = Quaternion.identity;
    //    }
    //}

    private void Awake()
    {
        _carControl = GetComponentInChildren<CarControl>();
    }

    private void Start()
    {
        currentTime = musicBoxCreateTime;
        MusicBoxesPreLoad();
    }

    private void Update()
    {
        if (_carControl.capacityFull) return;
        if (musicBoxCreateTime > 0) musicBoxCreateTime -= Time.deltaTime;
        if (musicBoxCreateTime <= 0)
        {
            CreateBox();
            boxIndex++;
            if(boxIndex >= 20)
            {
                boxIndex = 0;
                _carControl.CarMove();
            }
            musicBoxCreateTime = currentTime;
        }
    }

    private void CreateBox()
    {
        if (GetProductAmount() >= maxProductAmount) return;
        MusicBox musicBox = GetLastUnVisibilityMusicBox();
        musicBox.JumpToProductPosition();
        musicBox.SetVisibility(true);
    }

    private void MusicBoxesPreLoad()
    {
        int x, y = 0;

        for (int i = 0; i < musicBoxes.Count; i++)
        {
            x = i % 4;

            if (i % 4 == 0 && i != 0)
            {
                y++;
            }

            musicBoxes[i].SetVisibility(false);
            musicBoxes[i].SetPositions(Vector3.zero, new Vector3(x - xOffset, y, jumpPoseZ));
        }
    }

    private MusicBox GetLastUnVisibilityMusicBox()
    {
        foreach (MusicBox musicBox in musicBoxes)
        {
            if (!musicBox.Visibility)
            {
                return musicBox;
            }
        }

        return null;
    }

    private MusicBox GetLastVisibilityMusicBox()
    {
        for (int i = 0; i < musicBoxes.Count; i++)
        {

            if (!musicBoxes[i].Visibility)
            {
                if (i != 0)
                {
                    return musicBoxes[i - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        return null;
    }

    public void CloseBoxes(int amount, out List<Vector3> givenBoxesPositions)
    {
        List<Vector3> boxesPositions = new List<Vector3>();

        for (int i = 0; i < amount; i++)
        {
            MusicBox lastVisibilMusicBox = musicBoxes.Where(k => k.Visibility).LastOrDefault();

            if (GetLastVisibilityMusicBox() != null)
            {
                boxesPositions.Add(GetLastVisibilityMusicBox().transform.position);
                GetLastVisibilityMusicBox().SetVisibility(false);
            }
        }

        givenBoxesPositions = boxesPositions;
    }

    public int GetProductAmount()
    {
        for (int i = 0; i < musicBoxes.Count; i++)
        {
            if (!musicBoxes[i].Visibility)
            {
                return i;
            }
        }
        return musicBoxes.Count;
    }
}

