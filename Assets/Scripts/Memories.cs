using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Memories : MonoBehaviour
{
    public Volume localVolume;
    public string memoryText = " ";
    public string objectname = " ";
    public string pressE = "Press E to Interact";

    public GameObject memoryUI;
    public TextMeshProUGUI textMeshProUGUI;

    public float colorDuration = 1.5f;
    public float fadeDuration = 2.0f;

    private bool beenUsed = false;
    private bool playerInRange = false;
    private Renderer[] renderers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInRange || beenUsed)
            return;
        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        playerInRange = true;
        Interaction.Instance.ShowPrompt(pressE, objectname);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        playerInRange = false;
        Interaction.Instance.HidePrompt();
    }

    void Interact()
    {
        beenUsed = true;
        Interaction.Instance.HidePrompt();
        StartCoroutine(ColorThenFade());
    }
    IEnumerator ColorThenFade()
    {
        float t = 0f;
        while (t < colorDuration)
        {
            t += Time.deltaTime;
            localVolume.weight = Mathf.Lerp(0f, 1f, t / colorDuration);
            yield return null;
        }


        if (memoryUI != null && textMeshProUGUI != null)
        {
            textMeshProUGUI.text = memoryText;
            memoryUI.SetActive(true);
        }

        float elapsed = 0f;
        while (elapsed < 4f)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        if (memoryUI != null)
            memoryUI.SetActive(false);

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            localVolume.weight = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}

