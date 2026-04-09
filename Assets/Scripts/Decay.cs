using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Decay : MonoBehaviour
{
    public static Decay Instance;

    public float maxDecay = 100f;
    public float currentDecay = 100f;
    public float passiveDecayRate = 1.695f;

    public Slider decaySlider;
    public Image sliderFill;
    public Color healthyColor = Color.green;
    public Color dangerColor = Color.red;
    public Image fadeImage;
    private bool isEnding = false;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Start()
    {
        UpdateUI();
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0);
        StartCoroutine(FadeAndLoad());
    }

    void Update()
    {
        if (isEnding)
            return;

        float decay = Time.deltaTime * passiveDecayRate;

        LoseCognitive(decay);
    }
    
    void UpdateUI()
    {
        float ratio = currentDecay / maxDecay;

        if (decaySlider != null)
            decaySlider.value = ratio;

        if (sliderFill != null)
            sliderFill.color = Color.Lerp(dangerColor, healthyColor, ratio);

    }


    public void LoseCognitive(float decay)
    {
        currentDecay = Mathf.Clamp(currentDecay - decay, 0f, maxDecay);
        UpdateUI();

        if (currentDecay <= 0f)
        {
            TriggerEnding();
        }
    }
    void TriggerEnding()
    {
        if (isEnding)
            return;
        isEnding = true;
        SceneManager.LoadScene("Hospital Scene");
    }

    IEnumerator FadeAndLoad()
    {
        float t = 0f;
        while (t < 59f)
        {
            t += Time.deltaTime;
            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / 59f));
            yield return null;
        }
    }
}
