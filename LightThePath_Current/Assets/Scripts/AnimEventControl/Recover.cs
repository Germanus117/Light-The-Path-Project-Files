using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : MonoBehaviour {

    public bool recovering;

    public bool knockback;

    public void Recovered() {
        recovering = false;
    }

    public void Recovering() {
        recovering = true;
        knockback = true;
    }

    public void StopKnockback() {
        knockback = false;
    }
}
