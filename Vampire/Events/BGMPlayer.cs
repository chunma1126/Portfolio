using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private AudioSource _bgmPlayer;

    [SerializeField] private List<AudioClip> bgms;
    private int index = 0;
    
    
    private void Awake()
    {
        _bgmPlayer = GetComponent<AudioSource>();
        _bgmPlayer.volume = 0f;
    }

    private void Start()
    {
        _bgmPlayer.clip = bgms[index];
        _bgmPlayer.Play();

        StartCoroutine(FadeInVolume());
    }

    private void Update()
    {
        if (_bgmPlayer.isPlaying == false)
        {
            _bgmPlayer.clip = bgms[index++ % bgms.Count];
            _bgmPlayer.Play();
        }
    }
    private IEnumerator FadeInVolume()
    {
        float startVolume = _bgmPlayer.volume;
        float elapsedTime = 0f;

        while (elapsedTime < 3)
        {
            elapsedTime += Time.deltaTime;
            _bgmPlayer.volume = Mathf.Lerp(startVolume, 0.7f, elapsedTime / 3);
            yield return null;
        }

        _bgmPlayer.volume = 0.7f;
    }
}
