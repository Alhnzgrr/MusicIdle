using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float rotateSpeed;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private MobileInput _inputs;
    private Movements _movement;
    private EventData _eventData;
    private MusicBoxController _musicBoxController;

    private float horizontal;
    private float vertical;
    private float moneyTime = 0.5f;
    private bool isUpgradePanelOpen = false;
    internal bool isHandFull;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
        _rigidbody = GetComponent<Rigidbody>();
        _musicBoxController = GetComponentInChildren<MusicBoxController>();
        _animator = GetComponentInChildren<Animator>();
        _movement = new Movements(_rigidbody, transform);
        _inputs = new MobileInput();
        _eventData.Player = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UpgradeArea"))
        {
            if (!isUpgradePanelOpen)
            {
                _eventData.UpgradePanelOpen?.Invoke();
                isUpgradePanelOpen = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MoneysCollider"))
        {
            if (moneyTime <= 0)
            {
                other.GetComponent<PlayerMoneyRange>().PlayerTriggered(this.transform);
            }
            else
            {
                moneyTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UpgradeArea"))
        {
            if (isUpgradePanelOpen)
            {
                _eventData.UpgradePanelClose?.Invoke();
                isUpgradePanelOpen = false;
            }
        }
    }

    private void Update()
    {
        horizontal = _inputs.Horizontal;
        vertical = _inputs.Vertical;
        _animator.SetFloat("Velocity", Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        if (_musicBoxController.handFull)
        {
            if(_animator.GetBool("IsHandFull") == false)
            {
                _animator.SetBool("IsHandFull", true);
            }
        }
        else
        {
            if(_animator.GetBool("IsHandFull") == true)
            {
                _animator.SetBool("IsHandFull", false);
            }
        }
    }

    private void FixedUpdate()
    {
        _movement.Move(horizontal, horizontalSpeed, vertical, verticalSpeed);
        if (_inputs.LeftClick)
        {
            _movement.Rotate(horizontal, vertical, rotateSpeed);
        }
    }

    public void SpeedUpgrade()
    {
        verticalSpeed += 0.5f;
        horizontalSpeed += 0.5f;
    }

    public void HandAnimation()
    {
        _animator.SetBool("IsHandFull", isHandFull);
    }

}

