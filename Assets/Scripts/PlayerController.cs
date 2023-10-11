using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject character;
    public FollowPlayer mainCamera;
    public float moveSpeed;
    public float sprintSpeed;
    public float smooth = 0.1f;
    public float maxHealth;
    public float maxStamina;
    public float staminaDrain;
    public float restTime;
    
    public float currentHealth;
    public float currentStamina;
    private Animator anim;
    private CharacterController controller;
    private float playerSpeed;
    private bool resting;
    private Vector3 pointToLook;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        mainCamera = FindObjectOfType<FollowPlayer>();
        playerSpeed = moveSpeed;
        currentHealth = maxHealth; 
        currentStamina = maxStamina;
        resting = false;
    }

    private void Movement()
    {
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.forward * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.back * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.right * playerSpeed * Time.deltaTime);
        }
        //Sprinting functionality
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentStamina <= 0 )
            {
                playerSpeed = moveSpeed;
                StartCoroutine(GainStamina());
                return;
            }
            if (currentStamina > 0)
            {
                playerSpeed = sprintSpeed;
                currentStamina -= staminaDrain*Time.deltaTime;
            }
            if (resting)
            {
                return;
            }
            
        }
        else
        {
            playerSpeed = moveSpeed;
        }
        //Idle and Running animations
        if (!Input.anyKey)
        {
            anim.ResetTrigger("Run");
            anim.Play("m_weapon_idle_A");
        }
    }

    private void PlayerRotation()
    {
        //Player Rotation with mouse
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylength;
        if (groundPlane.Raycast(cameraRay, out raylength))
        {
            pointToLook = cameraRay.GetPoint(raylength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
    //Coroutine to regain stamina
    IEnumerator GainStamina()
    {
        resting = true;
        yield return new WaitForSeconds(restTime);
        currentStamina = maxStamina;
        resting = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        PlayerRotation();
    }
}
