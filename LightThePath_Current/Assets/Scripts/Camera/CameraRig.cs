using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRig : MonoBehaviour {
    //Input Names
    [System.Serializable]
    public class Inputs
    {
        public string mouseXInput;
        public string mouseYInput;
        public string controllerXInput;
        public string controllerYInput;
        public string camReturnInput;
        public string camZoomInput;
    }
    [System.Serializable]
    public class Transforms
    {
        public Transform camPivot;
        public Transform camRail;
        public Transform camTarget;
    }
    public Inputs inputs;
    public Transforms transforms;
    //Camera Rig And Target Transforms

    //Sensitivity Sliders

    public Slider VerticalCamSens;
    public Slider HorizontalCamSens;

    //-------------Camera Pivot----------------//
    //Camera Pivot Rotation Speeds

    [Header("Camera Pivot Speeds")]

    public float verticalSpeed;
    public float horizontalSpeed;
    public float camReturnSpeed = 20f;

    //Camera Pivot Y Rotation Clamps
    [Header("Camera Pivot Y Rotation Clamps")]
    [Tooltip("Clamps the Camera Pivot to the minimum set point")]
    public float clampMin = -20f;
    [Tooltip("Clamps the Camera Pivot to the max set point")]
    public float clampMax = 80f;
    //Camera Pivot Height
    [Header("Camera Pivot Height and Starting Angle")]
    [Tooltip("Camera Pivot height relative to target origin transform")]
    public float camPivotHeight = 1f;
    [Tooltip("Angle the Camera Pivot starts at on the X axis")]
    public float camStartingAngle = 10f;
    [Header("Camera Pivot Return Threshold")]
    [Tooltip("Stops the Camera Pivot when within set approximate range of the the targets rear transform")]
    public float returnThreshold = 1f;
    [Header("Invert Camera Pivot X Or Y Rotation")]
    public bool invertHorizontal;
    public bool invertVertical;
    //Camera Pivot Rotations X and Y
    float x;
    float y;
    //Camera Pivot Rotation Transform
    Quaternion rotation;
    //Camera Pivot Return Halt
    bool returning;


    //------------Camera Rail----------------//

    [Header("Camera Rail Distances")]

    //Camera Rail Distances
    public float camMinDistance = 2f;
    public float camMidDistance = 5f;
    public float camMaxDistance = 8f;
    //Camera Rail Speed
    [Header("Camera Rail Speed")]
    [Tooltip("Speed at which the Camera Rail moves from distance positions")]
    public float camRailSpeed = 10f;
    //Camera Rail Direction
    Vector3 railDirection;
    //Camera Rail Current Distance And Distance After Collision
    float currentDistance;
    float distance;
    //Camera Rail Set Distance
    bool zoomMin, zoomMid, zoomMax;


    private void Awake()
    {
        
        //------------Camera Pivot Initialization-----------//
        //Set Camera Pivot Rotation To Variables
        Vector3 angles = transforms.camPivot.eulerAngles;
        x = angles.x;
        y = angles.y;
        //Camera Pivot Y Starting Position Behind Player
        x = transforms.camTarget.eulerAngles.y;
        //Camera Pivot X Starting Angle
        y = camStartingAngle;

        //-----------Camera Rail Initialization------------//
        distance = transforms.camRail.localPosition.magnitude;
        railDirection = transforms.camRail.localPosition.normalized;
        //Set Camera Rail Starting Distance
        currentDistance = camMidDistance;
        zoomMid = true;

        Debug.Log("vertical speed is " + verticalSpeed);
        Debug.Log("horizontal speed is " + horizontalSpeed);


    }


    // Update is called once per frame
    void LateUpdate()
    {
        //Temporary Code Start
        /*if(Input.GetKeyDown(KeyCode.Y)) {
            invertHorizontal = !invertHorizontal;
        }
        if(Input.GetKeyDown(KeyCode.U)) {
            invertVertical = !invertVertical;
        }*/
        //Temporary Code End


        verticalSpeed = VerticalCamSens.value;
        horizontalSpeed = HorizontalCamSens.value;

        

        //------------Camera Pivot Rotation--------------//

        if (Input.GetButtonDown(inputs.camReturnInput))
        {
            returning = true;
            StartCoroutine(CamReturn());
        }


        if (Input.GetAxis(inputs.controllerXInput) != 0 || Input.GetAxis(inputs.controllerYInput) != 0)
        {
            returning = false;
        }
        if(!DeathPlane.playerFell) {
            transforms.camPivot.position = new Vector3(transforms.camTarget.position.x, transforms.camTarget.position.y + camPivotHeight, transforms.camTarget.position.z);
        }
        
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
        if(!DeathPlane.playerFell) {
            if(!CombatLock.targetLocked) {
                CameraPivotControllerRotation();
                rotation = Quaternion.Euler(y, x, 0f);

                y = ClampAngle(y, clampMin, clampMax);

                Quaternion interpRotation = Quaternion.Slerp(transforms.camPivot.localRotation, rotation, Time.deltaTime * 20);

                transforms.camPivot.rotation = interpRotation;
            } else {

                Quaternion rotation = Quaternion.LookRotation(CombatLock.selectedTarget.position - transforms.camPivot.position);
                CameraPivotControllerRotation();
                rotation = Quaternion.Euler(y, rotation.eulerAngles.y, 0f);

                y = ClampAngle(y, clampMin, clampMax);

                Quaternion interpRotation = Quaternion.Slerp(transforms.camPivot.localRotation, rotation, Time.deltaTime * 20);

                transforms.camPivot.rotation = interpRotation;


            }
        } else {
            Quaternion rotation = Quaternion.LookRotation(transforms.camTarget.position - transforms.camPivot.position);
            

            Quaternion interpRotation = Quaternion.Slerp(transforms.camPivot.localRotation, rotation, Time.deltaTime * 20);

            transforms.camPivot.rotation = interpRotation;


        }

        

        


        //-----------Camera Rail Distance And Collision------------//
        //Sets The Distance Of Camera Rail Based On Previous Distance
        if (Input.GetButtonDown(inputs.camZoomInput) && zoomMax)
        {
            zoomMin = true;
            zoomMid = false;
            zoomMax = false;
        }
        else if (Input.GetButtonDown(inputs.camZoomInput) && zoomMin)
        {
            zoomMin = false;
            zoomMid = true;
            zoomMax = false;
        }
        else if (Input.GetButtonDown(inputs.camZoomInput) && zoomMid)
        {
            zoomMin = false;
            zoomMid = false;
            zoomMax = true;
        }

        Vector3 desiredRailPosition = transforms.camPivot.TransformPoint(railDirection * camMaxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transforms.camPivot.position, desiredRailPosition, out hit))
        {
            Zoom();
            distance = Mathf.Clamp((hit.distance * 0.9f), 1f, currentDistance);

        }
        else
        {
            Zoom();
        }

        transforms.camRail.localPosition = Vector3.Lerp(transforms.camRail.localPosition, railDirection * distance, Time.deltaTime * camRailSpeed);
    }



    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360)
        {
            angle -= 360;
        }

        if (angle < -360)
        {
            angle += 360;
        }

        return Mathf.Clamp(angle, min, max);
    }

    void CameraPivotMouseRotation()
    {
        if (invertHorizontal)
        {
            x -= Input.GetAxis(inputs.mouseXInput) * horizontalSpeed;
        }
        else
        {
            x += Input.GetAxis(inputs.mouseXInput) * horizontalSpeed;
        }
        if (invertVertical)
        {
            y += Input.GetAxis(inputs.mouseYInput) * verticalSpeed;
        }
        else
        {
            y -= Input.GetAxis(inputs.mouseYInput) * verticalSpeed;
        }
    }

    void CameraPivotControllerRotation()
    {
        if (invertHorizontal)
        {
            x -= Input.GetAxis(inputs.controllerXInput) * horizontalSpeed;
        }
        else
        {
            x += Input.GetAxis(inputs.controllerXInput) * horizontalSpeed;
        }
        if (invertVertical)
        {
            y += Input.GetAxis(inputs.controllerYInput) * verticalSpeed;
        }
        else
        {
            y -= Input.GetAxis(inputs.controllerYInput) * verticalSpeed;
        }
    }

    IEnumerator CamReturn()
    {

        float targetRotation = transforms.camTarget.eulerAngles.y;

        while (!FastApproximately(x, targetRotation, returnThreshold))
        {

            x = Mathf.LerpAngle(transforms.camPivot.eulerAngles.y, targetRotation, Time.deltaTime * camReturnSpeed);
            if (!returning)
            {
                yield break;
            }
            yield return null;
        }


    }

    void Zoom()
    {
        if (zoomMin)
        {
            distance = camMinDistance;
        }

        if (zoomMid)
        {
            distance = camMidDistance;
        }

        if (zoomMax)
        {
            distance = camMaxDistance;
        }

        currentDistance = distance;
    }

    public static bool FastApproximately(float a, float b, float threshold)
    {
        return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
    }

    [ContextMenu("Match Target Rotation")]

    public void ChangeTransform()
    {
        float targetStartTransform = transforms.camTarget.eulerAngles.y;
        transforms.camPivot.eulerAngles = new Vector3(camStartingAngle, targetStartTransform, 0f);
        transform.position = new Vector3(transforms.camTarget.position.x, transforms.camTarget.position.y + camPivotHeight, transforms.camTarget.position.z);
        distance = transforms.camRail.localPosition.magnitude;
        railDirection = transforms.camRail.localPosition.normalized;
        transforms.camRail.localPosition = railDirection * camMidDistance;
    }

    


}
