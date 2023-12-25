using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCameraNearbyAction : MonoBehaviour {
    private ARCameraManager arCameraManager;
    public Transform arCameraTransform; // Reference to the AR camera's transform
    public float hideDistance = 0.1f;    // The distance at which the GameObject should be hidden


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
            // Log the camera position
            // Vector3 cameraPosition = arCameraManager.transform.position;
            Debug.Log("AR Camera Position: " + arCameraTransform.position);

            // Calculate the distance between the AR camera and this GameObject
            float distanceToCamera = Vector3.Distance(arCameraTransform.position, transform.position);
            Debug.Log("Distance to camera " + distanceToCamera);

            // Check if the distance is less than the hideDistance
            if (distanceToCamera < hideDistance) {
                Debug.LogError("Hit!");
                SetObjectVisibility(false);
            } else {
                SetObjectVisibility(true);
            }
        }
    }

    private void SetObjectVisibility(bool isVisible) {
        gameObject.SetActive(isVisible);
    }
}
