// [NAME] Erin Alvarico
// [Camera Controller (Level Selection)] Smooth transition to position (transform and rotation included) in a scene to view 
// appointed with camera script is attached

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControllerLevelSelection : MonoBehaviour
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
        // Will need to find another way to assign listeners to the buttons in a more organized way
        selections[0].onClick.AddListener(NoviceSelected);
        selections[1].onClick.AddListener(ProficientSelected);
        selections[2].onClick.AddListener(ExpertSelected);
        selections[3].onClick.AddListener(GoBack);
        selections[4].onClick.AddListener(GoBackProficient);
        selections[5].onClick.AddListener(GoBackExpert);
    }

    void Update()
    {
        // TESTING
        /*if(Input.GetKeyDown(KeyCode.Alpha1))
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
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentView = views[4];
        } */
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

    void NoviceSelected()
    {
        currentView = views[1];
        Debug.Log("view is now set to novice camera position.");
    }

    void ProficientSelected()
    {
        currentView = views[1];
        StartCoroutine(TransitionDelayTimeProficient());
        Debug.Log("view is now set to proficent camera position.");
    }

    IEnumerator TransitionDelayTimeProficient()
    {
        yield return new WaitForSeconds(1.3f);
        currentView = views[2];
    }

    void ExpertSelected()
    {
        currentView = views[1];
        StartCoroutine(TransitionDelayTimeExpert());
        Debug.Log("view is now set to expert camera position.");
    }

    IEnumerator TransitionDelayTimeExpert()
    {
        yield return new WaitForSeconds(1.3f);
        currentView = views[2];
        yield return new WaitForSeconds(1.3f);
        currentView = views[3];
    }

    void GoBack()
    {
        currentView = views[0];
        Debug.Log("view is now set to difficulty selection camera position.");
    }

    void GoBackProficient()
    {
        currentView = views[1];
        StartCoroutine(GoBackDelayProficient());
        Debug.Log("view is now set to difficulty selection camera position.");
    }

    IEnumerator GoBackDelayProficient()
    {
        yield return new WaitForSeconds(1f);
        currentView = views[0];
    }

    void GoBackExpert()
    {
        currentView = views[2];
        StartCoroutine(GoBackDelayExpert());
        Debug.Log("view is now set to difficulty selection camera position.");
    }

    IEnumerator GoBackDelayExpert()
    {
        yield return new WaitForSeconds(1f);
        currentView = views[1];
        yield return new WaitForSeconds(1f);
        currentView = views[0];
    }
}
