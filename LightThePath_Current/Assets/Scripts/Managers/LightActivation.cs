using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivation : MonoBehaviour
{
    public GameObject lightOrb;
    public int numLights;
    private Vector3 center;
    private Vector3 size;

    // Use this for initialization
    void Start ()
    {
        size = transform.localScale;
        center = transform.position;
        Invoke("SpawnLights", 0);
	}

    public void SpawnLights()
    {
        for(int i = 0; i < numLights; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            Instantiate(lightOrb, pos, Quaternion.identity);
        }
    }
}
