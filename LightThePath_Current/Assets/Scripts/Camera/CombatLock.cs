using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLock : MonoBehaviour {
    public List<Transform> targets;
    public static Transform selectedTarget;
    public Transform player;

    public float detectionRadius;

    public GameObject lockIcons;
    public GameObject softLockIcon;
    public GameObject targetLockIcon;
    

    public static bool targetLocked;
    [HideInInspector] public bool softLocked;

    float closestTargetDistance;
    float secondTargetDistance;


    //sets target to null to begin
    void Start()
    {
        selectedTarget = null;
    }


    private void SortEnemiesByDistance()
    {
        if (targets.Count > 0)
        {
            targets.RemoveAll(targets => targets == null);
            targets.Sort(delegate (Transform t1, Transform t2) {
                return Vector3.Distance(t1.position, player.transform.position).CompareTo(Vector3.Distance(t2.position, player.transform.position));
            });

        }
    }

    private void TargetEnemy()
    {
        if (targets.Count > 0)
        {
            if (selectedTarget == null)
            {
                selectedTarget = targets[0];
                
            }
            if(!targetLockIcon.activeInHierarchy) {
                targetLockIcon.SetActive(true);
            }
        }
    }





    private void CycleTarget()
    {
        if (targets.Count > 0)
        {
            int index = targets.IndexOf(selectedTarget);

            if (index < targets.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            DeselectTarget();
            selectedTarget = targets[index];
        }
    }

    // allows cycle through enemies but free movement
    private void SoftCycle()
    {
        if (targets.Count > 0)
        {
            int index = targets.IndexOf(selectedTarget);

            if (index < targets.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            DeselectTarget();
            selectedTarget = targets[index];
        }
    }

    //locks onto most recent cycled enemy without locking in movement 
    private void SoftLock()
    {
        if(targets.Count > 0) {
            if(selectedTarget == null) {
                selectedTarget = targets[0];
            }
            if(!softLockIcon.activeInHierarchy) {
                softLockIcon.SetActive(true);
            }
        }
    }


    private void DeselectTarget()
    {
        selectedTarget = null;
        
    }

    // updates enemy list and sorts locking by closest to farthest
    void Update()
    {
        AddObjects();
        if(selectedTarget != null) {
            lockIcons.transform.position = selectedTarget.transform.position + Vector3.up * 4f;
        }
        if (targets.Count > 0)
        {
            lockIcons.SetActive(true);
            SortEnemiesByDistance();
            
            lockIcons.transform.LookAt(Camera.main.transform);
            if (targetLocked == false)
            {
                SoftLock();
                SoftTargetLock();
                softLocked = true;
                targetLockIcon.SetActive(false);
            }
            else
            {
                softLocked = false;
                HardLock();
            }
        }
        else
        {
            selectedTarget = null;
            targetLocked = false;
            softLocked = false;
            softLockIcon.SetActive(true);
            lockIcons.SetActive(false);
        }
    }

    // creates collider for enemy detection and updates the list as it changes in range
    void AddObjects()
    {
        Collider[] locked = Physics.OverlapSphere(player.position, detectionRadius);
        targets = new List<Transform>();
        
        foreach (Collider obj in locked)
        {
            if (obj.tag == "Enemy" || obj.tag == "Object")
            {
                targets.Add(obj.transform);
            }
        }

        
    }


    //checking the range of enemies relative to the collider radious and which ones are closest and furthest away within the collider range
    private void SoftTargetLock()
    {
        if (InputManager.switchTarget)
        {
            SoftCycle();
        }

        if(selectedTarget != null) {
            float currentTarget = Vector3.Distance(player.transform.position, selectedTarget.transform.position);
            if(currentTarget > 20) {
                DeselectTarget();
                softLockIcon.SetActive(false);
                softLocked = false;
            }
        }

        if (InputManager.hardLock)
        {
            TargetEnemy();
            
            softLockIcon.SetActive(false);
            
            targetLocked = true;
        }
        
    }

    //this will be used to lock the player facing direction and camera onto the current hardlocked enemy
    private void HardLock()
    {
        float currentTarget = Vector3.Distance(player.transform.position, selectedTarget.transform.position);

        if (InputManager.hardLock)
        {
            targetLockIcon.SetActive(false);
            targetLocked = false;
        }

        if (InputManager.switchTarget)
        {
            CycleTarget();
        }

        if (currentTarget > detectionRadius)
        {
            DeselectTarget();

            targetLockIcon.SetActive(false);

            targetLocked = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(player.transform.position, detectionRadius);
    }

}