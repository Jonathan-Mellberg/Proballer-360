using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class settingsManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer; // Assign your mixer in the Inspector

    // --- AUDIO ---
    public void SetMasterVolume(float volume)
    {
        float minDB = -40f; // don’t go lower than -40, keeps it audible
        float dB = Mathf.Lerp(minDB, 0f, volume);

        // Optional: mute if slider is fully down
        if (volume <= 0.001f)
            dB = -80f;

        audioMixer.SetFloat("Master", dB);
    }
    public void SetMusicVolume(float volume)
    {
        float minDB = -40f; // don’t go lower than -40, keeps it audible
        float dB = Mathf.Lerp(minDB, 0f, volume);

        // Optional: mute if slider is fully down
        if (volume <= 0.001f)
            dB = -80f;

        audioMixer.SetFloat("Music", dB);
    }
    public void SetSFXVolume(float volume)
    {
        float minDB = -40f; // don’t go lower than -40, keeps it audible
        float dB = Mathf.Lerp(minDB, 0f, volume);

        // Optional: mute if slider is fully down
        if (volume <= 0.001f)
            dB = -80f;

        audioMixer.SetFloat("SFX", dB);
    }

    // --- GRAPHICS QUALITY ---
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // --- FULLSCREEN TOGGLE ---
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // --- RETURN TO ANOTHER SCENE (like Main Menu) ---
    public void BackToMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
