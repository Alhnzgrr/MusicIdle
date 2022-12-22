using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarControl : MonoBehaviour
{
    [SerializeField] Transform moveTransform;
    Animator _animator;
    internal bool capacityFull = false;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void CarMove()
    {
        StartCoroutine(CarMoveCoroutine());
    }

    IEnumerator CarMoveCoroutine()
    {
        capacityFull = true;
        _animator.SetBool("Close", true);
        transform.DOLocalMoveZ(moveTransform.position.z, 5f);

        yield return new WaitForSeconds(5f);

        transform.DOLocalMoveZ(0, 5f).OnComplete(() =>
        {
            _animator.SetBool("Close", false);
            capacityFull = false;
        });
    }
}
