using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicBox : MonoBehaviour
{
    private Vector3 _defaultPose;
    private Vector3 _targetPose;

    private MeshRenderer _meshRenderer;
    private bool visibility;
    public bool Visibility => visibility;
    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void JumpToProductPosition()
    {
        transform.localPosition = _defaultPose;
        transform.DOLocalJump(_targetPose, 1.5f, 1, 0.25f);
    }

    public void JumpToCollectPosition()
    {
        Vector3 localDefaultPosition = transform.parent.position - _defaultPose;
        transform.localPosition = localDefaultPosition;
        transform.DOLocalJump(_targetPose, 1.5f, 1, 0.25f);
    }

    public void SetVisibility(bool statu)
    {
        visibility = statu;
        _meshRenderer.enabled = visibility;
    }

    public void SetPositions(Vector3 defaultPose, Vector3 targetPose)
    {
        _defaultPose = defaultPose;
        _targetPose = targetPose;
    }

    public void SetDefaultPosition(Vector3 defaultPose)
    {
        _defaultPose = defaultPose;
    }
}
