// [NAME] Erin Alvarico
// [Camera Controller] Smooth transition to position (transform and rotation included) in a scene to view appointed with camera script is attached

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControllerQuizzes : MonoBehaviour
{
    // VARIABLES
    public Transform[] views;           // Array to store different points of view for camera (transforms)
    public float transitionSpeed;       // How fast the camera will pan to (location, rotation, etc.)
    Transform currentView;

    // VARIABLES - For mapping to buttons to change views
    public Button[] selections;


    // Start is called before the first frame update
    void Start()
    {
        // N/A
    }

    void Update()
    {
        // TESTING
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentView = views[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentView = views[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentView = views[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentView = views[3];
        }
    }

    // Update is called once per frame
    void LateUpdate()
    { 
        // Lerp Postion (smooth transform)
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        // Rotation
        Vector3 currentAngle = new Vector3(
            // x rotation
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            // y rotation
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            // z rotation
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        // Move to rotation of view
        transform.eulerAngles = currentAngle;
    } 
}
