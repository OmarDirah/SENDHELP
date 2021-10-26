using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRig))]
public class MouseControl : MonoBehaviour
{
    // Public variables 
    //Default Sensitivity 
    public float XSensitivity = 2f; 
    public float YSensitivity = 2f;

    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    // Private variables 
    private Quaternion yAxis;
    private Quaternion xAxis;
    private bool m_cursorIsLocked = true;
    private CameraRig rig;


    private void Start()
    {
        rig = GetComponent<CameraRig>();
        

    }

    private void Update()
    {
        Cursor.visible = true;
        // Check if left mouse button is being pressed and if the mouse is moving 
        if (Input.GetMouseButton(0) && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)) 
        {

            yAxis = rig.yAxis.localRotation;
            xAxis = rig.xAxis.localRotation;
            LookRotation();
            



        }
    }



    public void LookRotation()
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        yAxis *= Quaternion.Euler(0f, yRot, 0f);
        xAxis *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            xAxis = ClampRotationAroundXAxis(xAxis);

        if (smooth)
        {
            rig.yAxis.localRotation = Quaternion.Slerp(rig.yAxis.localRotation, yAxis,
                smoothTime * Time.deltaTime);
            rig.xAxis.localRotation = Quaternion.Slerp(rig.xAxis.localRotation, xAxis,
                smoothTime * Time.deltaTime);
        }
        else
        {
            rig.yAxis.localRotation = yAxis;
            rig.xAxis.localRotation = xAxis;
        }

        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
