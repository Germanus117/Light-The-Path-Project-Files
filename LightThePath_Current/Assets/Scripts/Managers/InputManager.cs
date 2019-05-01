using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [System.Serializable]
    public class InputNames {
        public string attack;
        public string block;
        public string switchTarget;
        public string hardLock;
        public string interact;
        public string frontBackMovement;
        public string leftRightMovement;
        public string sprint;
        public string pause;
    }
    public InputNames inputNames;

    public static bool attack;
    public static bool block;
    public static bool switchTarget;
    public static bool hardLock;
    public static bool interact;
    public static bool sprint;
    public static bool pause;
    public static float vertical;
    public static float horizontal;

    bool attackClick;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        vertical = Input.GetAxis(inputNames.frontBackMovement);
        horizontal = Input.GetAxis(inputNames.leftRightMovement);
        pause = Input.GetButtonDown(inputNames.pause);
        switchTarget = Input.GetButtonDown(inputNames.switchTarget);
        hardLock = Input.GetButtonDown(inputNames.hardLock);
        interact = Input.GetButtonDown(inputNames.interact);
        sprint = Input.GetButton(inputNames.sprint);

        if(Input.GetAxis(inputNames.attack) > 0f) {
            if(!attackClick) {
                attack = true;

                attackClick = true;
            } else {
                attack = false;
            }
        } else {
            attack = false;
            attackClick = false;
        }

        if(Input.GetAxis(inputNames.block) > 0f) {
            block = true;
        } else {
            block = false;
        }
	}
}
