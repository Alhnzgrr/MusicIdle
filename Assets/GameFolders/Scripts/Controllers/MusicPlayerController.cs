using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayerController : MonoBehaviour
{
    Animator _animator;
    MusicAreaMusicBoxes _musicArea;
    ParticleSystem _noteParticle;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _musicArea = GetComponentInParent<MusicAreaMusicBoxes>();
        _noteParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        _animator.SetInteger("Play", _musicArea.GetMusicBoxesAmount());
    }

    public void NotePlay()
    {
        if (!_noteParticle.isPlaying)
        {
            _noteParticle.gameObject.SetActive(true);
            _noteParticle.Play();
        }
    }

    public void NoteStop()
    {
        if (_noteParticle.isPlaying)
        {
            _noteParticle.transform.DOScale(Vector3.zero, 1.5f).OnComplete(() =>
            {
                _noteParticle.Stop();
                _noteParticle.gameObject.SetActive(false);
                _noteParticle.transform.localScale = Vector3.one;
            });
        }
    }
}
