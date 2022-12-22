using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public class MusicBoxController : MonoBehaviour
{
    [Header("Editor Music Box Creator Settings")]
    [SerializeField] int musicBoxAmount;
    [SerializeField] MusicBox musicBoxPrefab;
    [SerializeField] List<MusicBox> musicBoxes;
    [SerializeField] Transform musicBoxesParent;

    internal int collectBound = 7;
    internal int defaultCollectBound;
    internal bool handFull = false;

    private float spawnTime = 0.25f;

    //[Button("Create Music Boxes")]
    //private void CreateMusicBoxes()
    //{
    //    int count = musicBoxesParent.childCount;
    //    for (int i = 0; i < count; i++)
    //    {
    //        DestroyImmediate(musicBoxesParent.GetChild(0).gameObject);
    //    }

    //    musicBoxes.Clear();

    //    for (int i = 0; i < musicBoxAmount; i++)
    //    {
    //        MusicBox musicBox = (MusicBox)UnityEditor.PrefabUtility.InstantiatePrefab(musicBoxPrefab, musicBoxesParent.transform);

    //        musicBoxes.Add(musicBox);

    //        musicBox.transform.localPosition = Vector3.zero;
    //        musicBox.transform.rotation = Quaternion.identity;
    //    }
    //}

    private void Start()
    {
        PreloadMusicBoxes();
        defaultCollectBound = collectBound;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CollectableArea"))
        {
            if(spawnTime <= 0)
            {
                MusicBoxArea musicBOxArea = other.GetComponent<MusicBoxArea>();
                List<Vector3> receivedBoxesPositions = new List<Vector3>();

                musicBOxArea.CloseBoxes(collectBound, out receivedBoxesPositions);
                collectBound -= receivedBoxesPositions.Count;
                OpenMusicBoxes(receivedBoxesPositions);
                spawnTime = 0.25f;
            }
            else
            {
                spawnTime -= Time.deltaTime;
            }
        }
    }
    private void PreloadMusicBoxes()
    {
        for (int i = 0; i < musicBoxes.Count; i++)
        {
            musicBoxes[i].SetVisibility(false);
            musicBoxes[i].SetPositions(Vector3.zero, new Vector3(0, i , 0.5f));
        }
    }

    private void OpenMusicBoxes(List<Vector3> receivedBoxesPositions)
    {
        for (int i = 0; i < receivedBoxesPositions.Count; i++)
        {
            GetLastUnVisibilityMusicBox().SetDefaultPosition(receivedBoxesPositions[i]);
            GetLastUnVisibilityMusicBox().JumpToCollectPosition();
            GetLastUnVisibilityMusicBox().SetVisibility(true);
        }
        handFull = true;
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

    public void CloseBoxes(out List<Vector3> givenBoxesPositions)
    {
        List<Vector3> boxesPositions = new List<Vector3>();
        int amount = defaultCollectBound - collectBound;

        for (int i = 0; i < amount; i++)
        {
            if (GetLastVisibilityMusicBox() != null)
            {
                boxesPositions.Add(GetLastVisibilityMusicBox().transform.position);
                GetLastVisibilityMusicBox().SetVisibility(false);
            }
        }

        givenBoxesPositions = boxesPositions;
        handFull = false;
    }

    public void ResetCollectBound()
    {
        collectBound = defaultCollectBound;
    }
}
