using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public void Toggle()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
