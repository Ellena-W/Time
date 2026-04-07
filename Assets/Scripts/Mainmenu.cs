using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    [Header("Audio")]
    public AudioMixer audioMixer;


    public void StartGame()
    {
        SceneManager.LoadScene("Level 1 Generated"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); 
    }

    public void OpenSettings ()
    {
        settingsPanel. SetActive (true);
    }
    public void CloseSettings ()
    {
        settingsPanel.SetActive (false);
    }
     
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}