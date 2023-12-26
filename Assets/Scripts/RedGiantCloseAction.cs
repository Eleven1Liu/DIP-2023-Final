using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RedGiantCloseAction : MonoBehaviour {
    private ARCameraManager arCameraManager;
    public Transform arCameraTransform; // Reference to the AR camera's transform
    public float hideDistance = 5f;


    void OnEnable(){
        arCameraManager = FindObjectOfType<ARCameraManager>();
        if (arCameraManager != null){
            arCameraTransform = arCameraManager.transform;
            Debug.Log("Get AR Camera.");
        } else {
            Debug.LogError("AR Camera transform not assigned and AR Camera Manager not found.");
        }

        // Set hide distance to Y positon of the binary star system, default to y
        hideDistance = (float)(transform.position.y + 5e-2);
    }

    private void Update() {
        if (arCameraManager != null) {
            // Calculate the distance between the AR camera and this GameObject
            float distanceToCamera = Vector3.Distance(arCameraTransform.position, transform.position);
            Debug.Log("Distance to camera " + distanceToCamera);

            // Check if the distance is less than the hideDistance 
            // (i.e., when user is under the center)
            if (distanceToCamera < hideDistance) {
                Debug.LogError("Under the star system! Disappear");
                SetObjectVisibility(false);
            } 
            // else {
            //     SetObjectVisibility(true);
            // }
        }
    }

    private void SetObjectVisibility(bool isVisible) {
        gameObject.SetActive(isVisible);
    }
}
