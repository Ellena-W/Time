using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DisappearZone : MonoBehaviour
{
    public GameObject[] zoneObj;
    public float fadeInDur = 10f;
    public float fadeOutDur = 10f;

    private Dictionary<GameObject, Coroutine> activeCoroutines = new Dictionary<GameObject, Coroutine>();

    void Start()
    {
        foreach (GameObject obj in zoneObj)
        {
            if (obj == null)
                continue;

            foreach (Renderer rend in obj.GetComponentsInChildren<Renderer>())
            {
                SetMaterial(rend);
                SetAlpha(rend.material, 1f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger hit by: " + other.name);
        if (!other.CompareTag("Player"))
            return;

        foreach (GameObject obj in zoneObj)
        {
            if (obj == null) continue;
            StartFade(obj, false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (GameObject obj in zoneObj)
        {
            if (obj == null) continue;
            StartFade(obj, true);
        }
    }

    void StartFade(GameObject obj, bool fadeIn)
    {
        if (activeCoroutines.ContainsKey(obj) && activeCoroutines[obj] != null)
        {
            StopCoroutine(activeCoroutines[obj]);
        }

        Coroutine c;
        if (fadeIn)
            c = StartCoroutine(FadeIn(obj, fadeInDur));
        else
            c = StartCoroutine(FadeOut(obj, fadeOutDur));
        activeCoroutines[obj] = c;
    }

    IEnumerator FadeIn(GameObject obj, float duration)
    {
        obj.SetActive(true);

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) yield break;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duration);
            foreach (Renderer rend in renderers)
                SetAlpha(rend.material, alpha);
            yield return null;
        }

        foreach (Renderer rend in renderers)
            SetAlpha(rend.material, 1f);
    }

    IEnumerator FadeOut(GameObject obj, float duration)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) yield break;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            foreach (Renderer rend in renderers)
                SetAlpha(rend.material, alpha);
            yield return null;
        }

        foreach (Renderer rend in renderers)
            SetAlpha(rend.material, 0f);

        obj.SetActive(false);
    }

    void SetAlpha(Material mat, float alpha)
    {
        if (mat.HasProperty("_BaseColor"))
        {
            Color c = mat.GetColor("_BaseColor");
            c.a = alpha;
            mat.SetColor("_BaseColor", c);
        }
    }

    void SetMaterial(Renderer rend)
    {
        Material fadeMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        if (rend.material.HasProperty("_BaseColor"))
            fadeMat.SetColor("_BaseColor", rend.material.GetColor("_BaseColor"));
        if (rend.material.HasProperty("_BaseMap"))
            fadeMat.SetTexture("_BaseMap", rend.material.GetTexture("_BaseMap"));

        fadeMat.SetFloat("_Surface", 1);
        fadeMat.SetFloat("_Blend", 0);
        fadeMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        fadeMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        fadeMat.SetInt("_ZWrite", 0);
        fadeMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        fadeMat.renderQueue = 3000;

        rend.material = fadeMat;
    }
}