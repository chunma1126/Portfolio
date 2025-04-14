using UnityEngine;
using UnityEngine.Audio;

public class AudioChanger : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; 
    
    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
    }
}