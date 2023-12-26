using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aging : MonoBehaviour
{
    public GameObject collideTarget;
    public GameObject textTarget;
    public GameObject colorTarget1;
    public GameObject colorTarget2;
    public GameObject particleTarget;
    public string text;
    public Color color;
    public float colorIntensity;
    public float particleRateOverTime;
    public float particleNoiseStrength;
    public float particleNoiseFrequency;
    private bool isCollided = false;
    static private bool canvasBeingUsed = false;
    public void Start() {
        var factor = Mathf.Pow(2, colorIntensity);
        color = new Color(color.r * factor, color.g * factor, color.b * factor, 1f);
    }
    private void OnTriggerEnter(Collider collider)
    { 
        Debug.Log("Collide");
        GameObject collideWith = collider.gameObject;
        if (collideTarget == collideWith && isCollided == false) {
            isCollided = true;
            SetParticleTarget();
            SetColorTarget();
            StartCoroutine(ShowText());
        }
    }
    private IEnumerator ShowText() {
        var textComponent = textTarget.GetComponent<Text>();
        var originalColor = textComponent.color;
        float fadeDuration = 0.5f;
        float elapsedTime;
        while (canvasBeingUsed) {
            yield return null;
        }
        canvasBeingUsed = true;
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 
        
        elapsedTime = 0f;
        textComponent.text = text;
        while (elapsedTime < fadeDuration)
        {
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textComponent.text = "";
        canvasBeingUsed = false;
        Destroy(gameObject);
    }
    private void SetColorTarget() {
        Debug.Log("Current star color " + colorTarget1.GetComponent<Renderer>().material.GetColor("_EmissionColor"));
        var material = colorTarget1.GetComponent<Renderer>().material;
        material.SetColor("_EmissionColor", color);
        material = colorTarget2.GetComponent<Renderer>().material;
        material.SetColor("_EmissionColor", color);
        Debug.Log("Aging: The star color is set to " + color);
    }
    private void SetParticleTarget() {
        var particleSystem = particleTarget.GetComponent<ParticleSystem>();
        var particleEmissionModule = particleSystem.emission;
        var particleNoiseModule = particleSystem.noise;
        particleEmissionModule.rateOverTime = particleRateOverTime;
        particleNoiseModule.strength = particleNoiseStrength;
        particleNoiseModule.frequency = particleNoiseFrequency;
    }
}
