using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [Header("DATA")]
    [SerializeField] string AIName;
    internal bool isUnlock = false;

    private Animator _animator;
    private CheckAreas _checkArea;
    private MusicBoxController _musicBoxController;
    private NavMeshAgent _navMeshAgent;
    internal MusicAreas _musicAreas;
    private float checkTime = 0.5f;

    public int UnLockData
    {
        get => PlayerPrefs.GetInt(AIName, 1);
        set => PlayerPrefs.SetInt(AIName, value);
    }

    private void Awake()
    {
        _musicBoxController = GetComponent<MusicBoxController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _checkArea = GetComponentInParent<CheckAreas>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (UnLockData == 2)
        {
            gameObject.SetActive(true);
            isUnlock = true;
        }
        else
        {
            gameObject.SetActive(false);
            isUnlock = false;
        }
    }

    private void Update()
    {
        AnimatorController();
        if (checkTime <= 0)
        {
            if (CanStack())
            {
                _navMeshAgent.destination = _checkArea.GetLeastMusicArea().transform.position;
                _musicAreas = _checkArea.GetLeastMusicArea().MusicAreas;
                checkTime = 0.25f;
            }
            else
            {
                _navMeshAgent.destination = _checkArea.GetHighMusicBoxArea().noteTransform.position;
                checkTime = 0.25f;
            }
        }
        else
        {
            checkTime -= Time.deltaTime;
        }
    }

    private bool CanStack()
    {
        return _musicBoxController.defaultCollectBound - _musicBoxController.collectBound == _musicBoxController.defaultCollectBound;
    }

    public void UnlockHelper()
    {
        isUnlock = true;
        UnLockData = 2;
        gameObject.SetActive(true);
    }

    public void SpeedUpgrade()
    {
        _navMeshAgent.speed += 0.25f;
    }

    private void AnimatorController()
    {
        _animator.SetFloat("Velocity", _navMeshAgent.velocity.sqrMagnitude);
        if (_musicBoxController.handFull)
        {
            if (_animator.GetBool("IsHandFull") == false)
            {
                _animator.SetBool("IsHandFull", true);
            }
        }
        else
        {
            if (_animator.GetBool("IsHandFull") == true)
            {
                _animator.SetBool("IsHandFull", false);
            }
        }
    }
}
