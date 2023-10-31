using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicVolumeSlider;
    public Slider soundEffectsSlider;

    [Header("AudioSources")]
    public AudioSource musicAudioSource;
    public AudioSource soundEffectsAudioSource;

    // default value of 50% for every sound
    private float musicVolume = .5f;
    private float soundEffectsVolume = .5f;

    void Start()
    {
        // Getting the playerprefs values when starting game
        musicVolume = PlayerPrefs.GetFloat("musicPlaylistVolume");
        soundEffectsVolume = PlayerPrefs.GetFloat("soundEffectsVolume");

        // music playlist slider and audio source
        musicAudioSource.volume = musicVolume;
        musicVolumeSlider.value = musicVolume;

        // Sound effects slider and audio source
        soundEffectsAudioSource.volume = soundEffectsVolume;
        soundEffectsSlider.value = soundEffectsVolume;
    }

    void Update()
    {
        // music playlist
        musicVolume = musicVolumeSlider.value;
        musicAudioSource.volume = musicVolume;

        // sound effects
        soundEffectsVolume = soundEffectsSlider.value;
        soundEffectsAudioSource.volume = soundEffectsVolume;

        // Settings the values in 
        PlayerPrefs.SetFloat("musicPlaylistVolume", musicVolume);
        PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsVolume);
    }
}
