using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundEffectsSlider;

    [Header("AudioSources")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundEffectsAudioSource;

    // default value of 50% for every sound
    private float musicVolume = .5f;
    private float soundEffectsVolume = .5f;

    void Start()
    {
        // If the playerprefs key is not found set to our default value
        // If the playerprefs key is found it gets the set value
        if (!PlayerPrefs.HasKey("musicPlaylistVolume"))
        {
            PlayerPrefs.SetFloat("musicPlaylistVolume", musicVolume);
        } else musicVolume = PlayerPrefs.GetFloat("musicPlaylistVolume");

        if (!PlayerPrefs.HasKey("soundEffectsVolume"))
        {
            PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsVolume);
        } else soundEffectsVolume = PlayerPrefs.GetFloat("soundEffectsVolume");


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
