using UnityEngine;
using TMPro;

public class Memories : MonoBehaviour
{
    public string memoryText = " ";
    public string objectname = " ";
    public string pressE = "Press E to Interact";

    public GameObject memoryUI;
    public TextMeshProUGUI textMeshProUGUI;

    private bool beenUsed = false;
    private bool playerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if (!playerInRange || beenUsed)
            return;
        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    void OnTriggerEnter (Collider other)
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
        Decay.Instance.AddCognitive(Decay.Instance.memoryRestore);

        if (memoryUI != null && textMeshProUGUI != null)
        {
            textMeshProUGUI.text = memoryText;
            memoryUI.SetActive(true);
            StartCoroutine(HideMemoryUI());
        }
    }

    System.Collections.IEnumerator HideMemoryUI()
    {
        yield return new WaitForSeconds(4f);
        memoryUI.SetActive(false);
    }
    }
