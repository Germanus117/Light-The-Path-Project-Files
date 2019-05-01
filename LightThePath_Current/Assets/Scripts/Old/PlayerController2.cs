using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour
{
    //jump
    public float jumpForce;
    public float speed = 6;
    // is grounded
    public bool grounded;
    // direction of movement, default 0
    public Vector3 moveDirection = Vector3.zero;
    public bool isAttacking;
    public float speedDefault;

    Scene c_Scene;

    
    //Animator anim;

    private Rigidbody rb;

    
    private Vector3 SavePos;

    private Quaternion Saverot;

    void Start()
    {
        c_Scene = SceneManager.GetActiveScene();
        if(c_Scene.name == "HUB")
        {
            SavePos = new Vector3(PlayerPrefs.GetFloat("SavePosx"), PlayerPrefs.GetFloat("SavePosy"), PlayerPrefs.GetFloat("SavePosz"));
            Saverot = new Quaternion(PlayerPrefs.GetFloat("SaveRotx"), PlayerPrefs.GetFloat("SaveRoty"), PlayerPrefs.GetFloat("SaveRotz"), PlayerPrefs.GetFloat("SaveRotw"));
            if (SavePos != Vector3.zero & Saverot != new Quaternion(0, 0, 0, 0))
            {
                transform.position = SavePos;
                transform.rotation = Saverot;
            }
        }
        //Weapon.attackBool = false;
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

        void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SaveArea"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Debug.Log("Saving position");
                PlayerPrefs.SetFloat("SavePosx", transform.position.x);
                PlayerPrefs.SetFloat("SavePosy", transform.position.y);
                PlayerPrefs.SetFloat("SavePosz", transform.position.z);

                PlayerPrefs.SetFloat("SaveRotx", transform.rotation.x);
                PlayerPrefs.SetFloat("SaveRoty", transform.rotation.y);
                PlayerPrefs.SetFloat("SaveRotz", transform.rotation.z);
                PlayerPrefs.SetFloat("SaveRotw", transform.rotation.w);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
        }
        //isAttacking = Weapon.attackBool;
        if(isAttacking)
        {
            speed = 0f;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        //side steps
        //Left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(-1f, 0, 0);
        }
        //Right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(1f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) // jump
        {
            if (grounded == true)
            {
                Jump();
            }
        }
        speed = speedDefault;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
}