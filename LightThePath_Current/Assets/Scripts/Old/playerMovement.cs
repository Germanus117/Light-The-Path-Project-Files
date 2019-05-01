using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public float speed;
    //public float jumpForce = 8;
    //public bool grounded;
    public bool lightCollect;
    //public Text text;
    public GameObject lightBall;
    private Color colr;
	public bool isAttacking;

    //Animator anim;

    void Start()
    {
		//Weapon.attackBool = false;
        lightCollect = false;
        //colr = text.color;
        //anim = GetComponent<Animator>();
    }


    void Update()
    {
        Movement();
    }
    void Movement()
    {
		//isAttacking = Weapon.attackBool;
		if (isAttacking) 
		{
			speed = 0f;
		}
        //side steps
        //Left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(-0.08f, 0, 0);
        }
        //Right
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0.08f, 0, 0);
        }

        // Rotate / forward and back movment
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, -Vector3.up, speed * 100 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, Vector3.up, speed * 100 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }
		speed = 1;

        /*
        
        // Jump
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * Time.deltaTime * speed;
			Invoke ("gravity", 5);
        }

        */

    }

    
	/*
     
     public void gravity()
    {
		transform.position += Vector3.down * Time.deltaTime;

    }

    */
    
}




/* light pickup
void OnTriggerStay(Collider other)
{
 if (other.gameObject.CompareTag("Light"))
 {
     colr.a = 255.0f;
     if (Input.GetKey(KeyCode.LeftControl))
     {
         lightCollect = true;
         Destroy(lightBall);
     }
 }
}
*/
