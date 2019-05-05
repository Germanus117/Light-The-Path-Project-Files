using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightPath : MonoBehaviour
{

    //public int pathLights;
    public List<GameObject> lights = new List<GameObject>();

    public int LightPathID;
    public ShrineManager activateShrine;

    // Test for player being
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            lights.Add(child.gameObject);
        }
    }
    public IEnumerator TurnOn()
    {
        bool notOn = true;
        while (notOn)
        {
            for (int i = 0; i < lights.Count; i++)
            {
                if (!lights[i].activeInHierarchy)
                {
                    notOn = true;
                    lights[i].SetActive(true);
                }
                else
                {
                    notOn = false;
                    break;
                }
            }

            yield return null;
        }

    }

    public IEnumerator TurnOff()
    {
        bool notOff = true;
        while (notOff)
        {
            for (int i = 0; i < lights.Count; i++)
            {
                if (lights[i].activeInHierarchy)
                {
                    notOff = true;
                    lights[i].SetActive(false);
                    if (activateShrine.shrineID == LightPathID)
                    {
                        activateShrine.orbUsed = true;
                    }
                }
                else
                {
                    notOff = false;
                    break;
                }
            }

            yield return null;
        }
    }



}
