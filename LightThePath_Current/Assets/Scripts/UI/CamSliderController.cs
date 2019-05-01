using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamSliderController : MonoBehaviour {

    public Slider VerticalCamSens;
    public Slider HorizontalCamSens;

    public float currentVerticalSpeed;
    public float currentHorizontalSpeed;

    public CameraRig CameraRigScript;

    public float verticalSpeed;
    public float horizontalSpeed;

    // Use this for initialization
    void Start () {

        verticalSpeed = CameraRigScript.verticalSpeed;
        horizontalSpeed = CameraRigScript.horizontalSpeed;

        VerticalCamSens.value = verticalSpeed;
        HorizontalCamSens.value = horizontalSpeed;

	}
	
	// Update is called once per frame
	void Update () {

        VerticalCamSens.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        HorizontalCamSens.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        currentHorizontalSpeed = HorizontalCamSens.value;
        currentVerticalSpeed = VerticalCamSens.value;

        //Debug.Log(VerticalCamSens.value);
        //Debug.Log(HorizontalCamSens.value);
    }
}
