using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player movement vector
    Vector3 movement;

    //Character Controller used for movement
    CharacterController controller;
    public float gravity = 12f;
    public float normalSpeed;
    public float sprintingSpeed;
    public float shieldSpeed;
    public float lockSpeed;
    public float shieldWalkSpeed;
    public static float speedBoost = 0f;



    public float speedSmoothing;
    float speedSmoothVelocity;
    float currentSpeed;

    public float turnSmoothing;
    float velocityY;

    float turnSmoothVelocity;
    float speed;

	//check if player is moving
	public bool playerMoving;
	Vector3 lastPosition;

    //public bool targetLocked;

    Transform mainCam;


    Animator anim;

    public CombatLock combatLock;

    Stamina stamina;

    bool walking;

    public AudioSource StepSound;
    //public AudioSource AttackSound;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main.transform;
        stamina = GetComponent<Stamina>();
        StepSound.enabled = false;
        StepSound.loop = false;

    }


    // Update is called once per frame
    void Update()
    {
		//Check if player is moving
		if (lastPosition != gameObject.transform.position) {
			playerMoving = true;
		} else {
			playerMoving = false;
		}
		lastPosition = gameObject.transform.position;
        if (!Weapon.attacking)
        {


            if (PlayerDamage.shielding)
            {
                speed = shieldSpeed;
                anim.SetLayerWeight(anim.GetLayerIndex("Combat"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Combat")), 1f, Time.deltaTime * 5f));
                anim.SetBool("Shielding", true);
                StepSound.enabled = false;
                anim.SetFloat("ShieldorMove", Mathf.Lerp(anim.GetFloat("ShieldorMove"), 1f, Time.deltaTime * 5f));
            }
            else
            {
				if (InputManager.sprint && stamina.currentStamina != 0 && !StaminaBoost.infStamina && playerMoving == true && !PlayerDamage.tookDamage)
                {
                    speed = sprintingSpeed + speedBoost;
                    stamina.staminaInUse = true;

                }
                else if (InputManager.sprint && StaminaBoost.infStamina)
                {
                    speed = sprintingSpeed + speedBoost;
                }
                else
                {
                    speed = normalSpeed + speedBoost;                    
                    stamina.staminaInUse = false;
                }


                anim.SetBool("Shielding", false);
                anim.SetFloat("ShieldorMove", Mathf.Lerp(anim.GetFloat("ShieldorMove"), 0f, Time.deltaTime * 5f));
                anim.SetLayerWeight(anim.GetLayerIndex("Combat"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Combat")), 0f, Time.deltaTime * 5f));
            }



            NormalMovement();
            
        }
        else
        {
            AttackMovement();
            anim.SetBool("Shielding", false);
            anim.SetFloat("ShieldorMove", Mathf.Lerp(anim.GetFloat("ShieldorMove"), 0f, Time.deltaTime * 5f));
            anim.SetLayerWeight(anim.GetLayerIndex("Combat"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Combat")), 0f, Time.deltaTime * 5f));
            
        }


        
    }

    private void FixedUpdate()
    {
        PlayFootsteps();
    }

    public void NormalMovement()
    {
        Vector2 input = new Vector2(InputManager.horizontal, InputManager.vertical);
        float inputDir = input.magnitude;
        inputDir = Mathf.Clamp(inputDir, -1f, 1f);

        if (input != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothing);
        }


        float targetSpeed = speed * inputDir;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothing);

        currentSpeed = Mathf.Clamp(currentSpeed, 0f, speed);

        velocityY += Time.deltaTime * -gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0f;
        }

        float animationSpeedPercent = inputDir;


        if (PlayerDamage.shielding)
        {
            anim.SetFloat("ShieldWalk", animationSpeedPercent, speedSmoothing, Time.deltaTime);
        }
        else
        {
            if (InputManager.sprint && stamina.currentStamina != 0)
            {
                anim.SetFloat("WalktoRun", animationSpeedPercent * 2f, speedSmoothing, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("WalktoRun", animationSpeedPercent, speedSmoothing, Time.deltaTime);
            }
        }

    }

    void AttackMovement()
    {
        Vector2 input = new Vector2(InputManager.horizontal, InputManager.vertical);
        Vector2 inputDir = input;

        if (inputDir != Vector2.zero && Weapon.canTurn)
        {
            //AttackSound.Play();
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, Weapon.turnSmooth);
        }


        float targetSpeed = Weapon.speed;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothing);

        Vector3 velocity = transform.forward * currentSpeed;

        controller.Move(velocity * Time.deltaTime);
    }

    public void PlayFootsteps()
    {
        if (currentSpeed > 0.5f)
        {
            StepSound.enabled = true;
            StepSound.loop = true;
        }
        if (currentSpeed < 0.5f)
        {
            StepSound.enabled = false;
            StepSound.loop = false;
        }
    }

    public void Knockback() {
        speed = normalSpeed / 5f;
    }

    public void Normal() {
        speed = normalSpeed;
    }
}