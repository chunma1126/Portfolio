using UnityEngine;

[System.Serializable]
public struct ClipName
{
    public AudioClip audioClip;
    public string name;
}

public class SoundEffect : MonoBehaviour
{
    private AudioSource _sfxPlayer;

    [SerializeField] private ClipName[] sfx;
    
    private void Start()
    {
        _sfxPlayer = GetComponent<AudioSource>();
    }

    public void SfxPlay(string _sfxName)
    {
        AudioClip audioClip = null;
        foreach (var item in sfx)
        {
            if (item.name == _sfxName)
            {
                audioClip = item.audioClip;
                break;
            }
        }

        _sfxPlayer.PlayOneShot(audioClip);
    }
}
