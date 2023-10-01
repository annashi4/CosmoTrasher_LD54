using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private AudioMixer _mixer;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _hookshotAsteroidClip;
    
    
    private Tween _cutoffTween;
    
    private void Start()
    {
        _mixer.SetFloat("Cutoff", 22000);
    }

    public void SetVolume(float volume)
    {
        _mixer.SetFloat("Volume", volume);
    }

    public void MakeCutoff()
    {
        _cutoffTween.Kill();
        _cutoffTween = _mixer.DOSetFloat("Cutoff", 800, 1f);
    }

    public void UndoCutoff()
    {
        _cutoffTween.Kill();
        _cutoffTween = _mixer.DOSetFloat("Cutoff", 22000, 1f);
    }
}