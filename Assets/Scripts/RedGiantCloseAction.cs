using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class RedGiantCloseAction : MonoBehaviour {
    private ARCameraManager arCameraManager;
    public Transform arCameraTransform; // Reference to the AR camera's transform
    public float hideDistance = 5f - 1.6f; // Y-axis - average human height
    private float DEFAULT_NEBULA_SHAPE_RADIUS = 10f;
    private float FINAL_NEBULA_SHAPE_RADIUS = 1f;

    private float NEBULA_LIGHT_UP_RATIO = 5.0f;
    
    private bool isSet = false;
    private List<ParticleSystem> particleSystemList = new List<ParticleSystem>();
    private List<ParticleSystem> explosionList = new List<ParticleSystem>();
    

    void Start() {
        // Set distance for hiding the binary star system
        hideDistance = (float)(transform.position.y - 1.6f);
        GameObject[] nebulaGameObjs = GameObject.FindGameObjectsWithTag("Nebula");
        foreach (GameObject obj in nebulaGameObjs) {
            ParticleSystem nebula = obj.GetComponent<ParticleSystem>();
            particleSystemList.Add(nebula);
        }

        // Explosion
        GameObject[] explosionObjs = GameObject.FindGameObjectsWithTag("Explosion");
        foreach (GameObject obj in explosionObjs) {
            ParticleSystem explosionSys = obj.GetComponent<ParticleSystem>();
            explosionList.Add(explosionSys);
        }
    }

    void OnEnable(){
        arCameraManager = FindObjectOfType<ARCameraManager>();
        if (arCameraManager != null){
            arCameraTransform = arCameraManager.transform;
            Debug.Log("Get AR Camera.");
        } else {
            Debug.LogError("AR Camera transform not assigned and AR Camera Manager not found.");
        }
    }

    private void Update() {
        if (arCameraManager != null) {
            // Calculate the distance between the AR camera and this GameObject
            float distanceToCamera = Vector3.Distance(arCameraTransform.position, transform.position);
            Debug.Log("Distance to camera " + distanceToCamera);

            // Check if the distance is less than the hideDistance 
            // (i.e., when user is under the center)
            if (distanceToCamera < hideDistance && !isSet) {
                isSet = true;
                
                Debug.LogError("Under the star system! Disappear");
                PlayRedGiantEnd();
            }
        }
    }

    private void PlayRedGiantEnd(){
        // Play explosion
        foreach (ParticleSystem explosionSys in explosionList) {
            explosionSys.Play();
        }
        // Wait for the duration of the first Particle System (not working)
        // yield return new WaitForSeconds(explosionList[0].main.duration*2f);

        SetObjectVisibility(false);

        // Light up the sky: Get all partial systems tagged Nebula and set smaller radius.
        foreach (ParticleSystem nebulaSys in particleSystemList) {
            SetShapeRadius(FINAL_NEBULA_SHAPE_RADIUS, nebulaSys);
            SetStartSize(nebulaSys);
            nebulaSys.Play(); // reset particle system
        }
    }

    private void SetObjectVisibility(bool isVisible) {
        gameObject.SetActive(isVisible);
    }

    private void SetShapeRadius(float radius, ParticleSystem nebulaSys){
        // Set the radius of the Particle System's shape of nebula
        ParticleSystem.ShapeModule shapeModule = nebulaSys.shape;
        shapeModule.radius = radius;
    }

    private void SetStartSize(ParticleSystem nebulaSys) {
        // Access the MainModule of the ParticleSystem
        ParticleSystem.MainModule mainModule = nebulaSys.main;
        Debug.Log(mainModule.startSize.constantMin);
        Debug.Log(mainModule.startSize.constantMax);

        float minSize = mainModule.startSize.constantMin;
        float maxSize = mainModule.startSize.constantMax;

        mainModule.startSize = new ParticleSystem.MinMaxCurve(minSize * NEBULA_LIGHT_UP_RATIO, maxSize * NEBULA_LIGHT_UP_RATIO);
    }

    void OnApplicationQuit() {
        ResetSettings();
    }

    void ResetSettings() {
        // Red Giant 
        SetObjectVisibility(true);

        // Nebulas
        foreach (ParticleSystem nebulaSys in particleSystemList) {
            SetShapeRadius(DEFAULT_NEBULA_SHAPE_RADIUS, nebulaSys);
        }
    }
}
