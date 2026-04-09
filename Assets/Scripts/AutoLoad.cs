using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadScene : MonoBehaviour
{
    public string nextScene;
    public float delay = 5f;

    void Start()
    {
        Invoke(nameof(LoadNext), delay);
    }

    void LoadNext()
    {
        SceneManager.LoadScene("UI UX 2");
    }
}