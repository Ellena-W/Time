using UnityEngine;
using System.Collections;

public class SkyboxCycle : MonoBehaviour
{
    public Cubemap[] skyboxes; // Assign 4 skybox cubemaps in Inspector
    public Material blendedSkyboxMaterial; // Material with custom skybox blend shader
    public float blendDuration = 5f; // Time to blend
    public float delayBetweenTransitions = 10f; // Wait time before next blend

    private int currentIndex = 0;
    private int nextIndex = 1;

    void Start()
    {
        if (skyboxes.Length < 2) {
            Debug.LogWarning("Need at least 2 skyboxes.");
            Debug.Log("Current Skybox: " + skyboxes[currentIndex].name + " -> Next: " + skyboxes[nextIndex].name);
            return;
        }

        // Set initial textures
        blendedSkyboxMaterial.SetTexture("_Skybox1", skyboxes[currentIndex]);
        blendedSkyboxMaterial.SetTexture("_Skybox2", skyboxes[nextIndex]);
        blendedSkyboxMaterial.SetFloat("_Blend", 0f);

        RenderSettings.skybox = blendedSkyboxMaterial;
        DynamicGI.UpdateEnvironment();

        StartCoroutine(BlendLoop());
    }

    IEnumerator BlendLoop()
    {
        while (true)
        {
            float t = 0f;

            // Blend from _Skybox1 to _Skybox2
            while (t < 1f)
            {
                t += Time.deltaTime / blendDuration;
                blendedSkyboxMaterial.SetFloat("_Blend", Mathf.Clamp01(t));
                DynamicGI.UpdateEnvironment();
                yield return null;
            }

            // Prepare next pair
            currentIndex = (currentIndex + 1) % skyboxes.Length;
            nextIndex = (currentIndex + 1) % skyboxes.Length;

            blendedSkyboxMaterial.SetTexture("_Skybox1", skyboxes[currentIndex]);
            blendedSkyboxMaterial.SetTexture("_Skybox2", skyboxes[nextIndex]);
            blendedSkyboxMaterial.SetFloat("_Blend", 0f);
            DynamicGI.UpdateEnvironment();

            yield return new WaitForSeconds(delayBetweenTransitions);
        }
    }
}
