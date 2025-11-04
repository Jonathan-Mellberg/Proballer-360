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
        // Assuming your mixer has a parameter named "MasterVolume"
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        // Assuming your mixer has a parameter named "MasterVolume"
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        // Assuming your mixer has a parameter named "MasterVolume"
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
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
