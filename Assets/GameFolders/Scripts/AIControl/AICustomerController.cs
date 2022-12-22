using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICustomerController : MonoBehaviour
{
    [SerializeField] Transform moveArea;
    [SerializeField] float checkTime;
    [SerializeField] bool isSittingCustomer;

    private Animator _animator;
    private Vector3 _defaultTransform;
    private NavMeshAgent _navMeshAgent;
    private MusicAreaMusicBoxes _musicArea;
    private float defaultTime;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _musicArea = GetComponentInParent<MusicAreaMusicBoxes>();
    }

    private void Start()
    {
        defaultTime = checkTime;
        transform.LookAt(_musicArea.transform);
    }

    private void Update()
    {
        if (checkTime > 0)
        {
            checkTime -= Time.deltaTime;
        }
        else
        {
            if (CustomerStop())
            {
                if (isMusicPlay())
                {
                    MusicPlayEffect();
                }
                else
                {
                    MusicStopEffect();
                }
            }
            else
            {
                checkTime = defaultTime;

            }
        }
    }

    private bool isMusicPlay()
    {
        return _musicArea.GetMusicBoxesAmount() > 0;
    }

    private bool CustomerStop()
    {
        return _navMeshAgent.velocity.sqrMagnitude == 0;
    }

    private void MusicPlayEffect()
    {
        if (_animator.GetBool("Dance") == false) _animator.SetBool("Dance", true);
        checkTime = defaultTime;
    }

    private void MusicStopEffect()
    {
        if (_animator.GetBool("Dance") == true) _animator.SetBool("Dance", false);
        checkTime = defaultTime;
    }

}
