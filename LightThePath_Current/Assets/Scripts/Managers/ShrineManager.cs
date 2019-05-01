using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineManager : MonoBehaviour
{
    public GameObject[] zones;
    public bool orbUsed;
    public GameObject darkness;
    //public LightActivation turnOn;
    public GameObject lightProbes;
    public GameObject InteractDialog;

    private void Update()
    {
        if(orbUsed)
        {
            for(int i = 0; i < zones.Length; i++)
            {
                zones[i].SetActive(true);
            }
            darkness.SetActive(false);
            lightProbes.SetActive(true);
            InteractDialog.SetActive(false);

            orbUsed = false;
        }
    }
}
