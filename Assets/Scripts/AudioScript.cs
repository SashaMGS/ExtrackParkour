using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource _musicAS;
    public AudioSource _audioAS;
    public AudioClip[] _musicClips;
    public AudioClip _notClip;
    public AudioClip _clickClip;
    public AudioClip _onDragClip;
    public AudioClip _pickUpClip;

    private void Start()
    {
        _musicAS = GetComponent<AudioSource>();
        _musicAS.clip = _musicClips[Random.Range(0, _musicClips.Length)];
        _musicAS.Play();
    }

    private void Update()
    {
        _musicAS.volume = GetComponent<Saves>()._audio;
        _audioAS.volume = GetComponent<Saves>()._audio;
    }

    public void MuteAudio()
    {
        _musicAS.mute = true;
    }

    public void UnMute()
    {
        _musicAS.mute = false;
    }

    public void DoAudio(string name)
    {
        if (name == "not")
            _audioAS.clip = _notClip;
        else if (name == "click")
            _audioAS.clip = _clickClip;
        else if (name == "ondrag")
            _audioAS.clip = _onDragClip;
        else if (name == "pickup")
            _audioAS.clip = _pickUpClip;
        _audioAS.Play();
    }
}
