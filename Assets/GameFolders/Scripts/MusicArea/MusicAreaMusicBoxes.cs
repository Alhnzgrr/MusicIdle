using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public class MusicAreaMusicBoxes : MonoBehaviour
{
    [Header("Editor Music Box Creator Settings")]
    [SerializeField] int musicBoxAmount;
    [SerializeField] MusicBox musicBoxPrefab;
    [SerializeField] List<MusicBox> musicBoxes;
    [SerializeField] Transform musicBoxesParent;

    [Header("Run Time Music Box Settings")]
    [SerializeField] float jumpPoseZ;
    [SerializeField] float xOffset;
    [SerializeField] MusicAreas musicArea;

    MusicAreaCollider _musicAreaCollider;
    MusicPlayerController _musicPlayer;
    EventData _eventData;
    public MusicAreas MusicAreas => musicArea;

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
    private void Awake()
    {
        _musicAreaCollider = GetComponent<MusicAreaCollider>();
        _musicPlayer = GetComponentInChildren<MusicPlayerController>();
    }

    private void Start()
    {
        MusicBoxesPreLoad();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_musicAreaCollider.IsUnclock) return;

        if (other.CompareTag("AI"))
        {
            if (other.GetComponent<AIController>()._musicAreas == musicArea)
            {
                if (!other.GetComponent<MusicBoxController>().handFull) return;
                MusicBoxController musicBoxController = other.GetComponent<MusicBoxController>();
                List<Vector3> receivedBoxesPositions = new List<Vector3>();
                musicBoxController.CloseBoxes(out receivedBoxesPositions);
                musicBoxController.ResetCollectBound();
                OpenMusicBoxes(receivedBoxesPositions);
            }
        }
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<MusicBoxController>().handFull) return;
            MusicBoxController musicBoxController = other.GetComponent<MusicBoxController>();
            List<Vector3> receivedBoxesPositions = new List<Vector3>();
            musicBoxController.CloseBoxes(out receivedBoxesPositions);
            musicBoxController.ResetCollectBound();
            OpenMusicBoxes(receivedBoxesPositions);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_musicAreaCollider.IsUnclock)
            {
                _musicAreaCollider.ColliderSettings();
            }
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
        _musicPlayer.NotePlay();
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
            if (GetLastVisibilityMusicBox() != null)
            {
                boxesPositions.Add(GetLastVisibilityMusicBox().transform.position);
                GetLastVisibilityMusicBox().SetVisibility(false);
            }
        }

        givenBoxesPositions = boxesPositions;
    }

    public void CloseBoxes(int amount)
    {
        List<Vector3> boxesPositions = new List<Vector3>();

        for (int i = 0; i < amount; i++)
        {
            if (GetLastVisibilityMusicBox() != null)
            {
                boxesPositions.Add(GetLastVisibilityMusicBox().transform.position);
                GetLastVisibilityMusicBox().SetVisibility(false);
            }
        }
        if(GetMusicBoxesAmount() == 0)
        {
            _musicPlayer.NoteStop();
        }
    }

    public int GetMusicBoxesAmount()
    {
        for (int i = 0; i < musicBoxes.Count; i++)
        {
            if (!musicBoxes[i].Visibility)
            {
                return i;
            }
        }
        return 0;
    }
}
