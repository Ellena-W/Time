using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Decay : MonoBehaviour
{
    public static Decay Instance;

    public float maxDecay = 100f;
    public float currentDecay = 100f;
    public float passiveDecayRate = 5f;
    public float forwardMovementDecay = 3f;
    public float memoryRestore = 20f;

    public Slider decaySlider;
    public Image sliderFill;
    public Color healthyColor = Color.green;
    public Color dangerColor = Color.red;

    private bool isEnding = false;
    private bool forwardMovement = false;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (isEnding)
            return;

        float decay = Time.deltaTime * passiveDecayRate;

        if (forwardMovement)
            decay += forwardMovementDecay * Time.deltaTime;

        ReduceDecay(decay);
    }
    public void SetMovingForward(bool movingForward)
    {
        forwardMovement = movingForward;
    }
    void UpdateUI()
    {
        float ratio = currentDecay / maxDecay;

        if (decaySlider != null)
            decaySlider.value = ratio;

        if (sliderFill != null)
            sliderFill.color = Color.Lerp(dangerColor, healthyColor, ratio);

    }

    public void ReduceDecay(float restore)
    {
        currentDecay = Mathf.Clamp(currentDecay - restore, 0f, maxDecay);
        UpdateUI();

        if (currentDecay <= 0f)
        {
            TriggerEnding();
        }



        void TriggerEnding()
        {
            if (isEnding)
                return;
            isEnding = true;

            SceneManager.LoadScene("Ending");
        }
    }
}
