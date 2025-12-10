using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public Slider volume;
    public AudioMixer audioMixer;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Master", 1f);
        volume.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float value)
    {
        float vol = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("Master", vol);
        PlayerPrefs.SetFloat("Master", value);
        PlayerPrefs.Save();
    }
}
