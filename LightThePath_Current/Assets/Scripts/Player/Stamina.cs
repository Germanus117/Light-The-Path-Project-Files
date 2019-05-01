using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {


    public int maxStamina;
    public int currentStamina;

    public float timeTillRegen;
    public float regenSpeed;
    public float staminaUseSpeed;

    public bool staminaInUse;

    public Slider staminaSlider;

    [HideInInspector]public float timer;

    bool regen;
    bool subracting;

	// Use this for initialization
	void Start () {
        currentStamina = maxStamina;
        timer = timeTillRegen;
	}
	
	// Update is called once per frame
	void Update () {
        if(currentStamina < maxStamina && !staminaInUse) {
            if(timer > 0f) {
                timer -= Time.deltaTime;
                CancelInvoke("RegenStamina");
                regen = false;
            } else if (!regen) {
                InvokeRepeating("RegenStamina", 0f, regenSpeed);
                regen = true;
            }
        } else {
            CancelInvoke("RegenStamina");
            timer = timeTillRegen;
            regen = false;
        }

        if(staminaInUse) {
            if(!subracting) {
                InvokeRepeating("SubtractStamina", 0f, staminaUseSpeed);
                subracting = true;
            }
        } else {
            CancelInvoke("SubtractStamina");
            subracting = false;
        }
        if(currentStamina <= 0) {
            currentStamina = 0;
        }

        staminaSlider.value = currentStamina / (maxStamina * 1f);
	}

    

    public void RegenStamina() {
        currentStamina++;
        if(currentStamina >= maxStamina) {
            CancelInvoke("RegenStamina");
            currentStamina = maxStamina;
        }
    }


    public void SubtractStamina() {
        currentStamina--;
        if(currentStamina <= 0) {
            CancelInvoke("SubtractStamina");
        }
    }
}
