using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameUI gameUI;
    public FollowPlayer follow;
    private GameObject character;
    public float moveSpeed;
    public float sprintSpeed;
    public float smooth = 0.1f;
    public float maxHealth;
    public float maxStamina;
    public float staminaDrain;
    public float restTime;
    public float deathDelay;
    public GameObject rifle;
    public GameObject pistol;
    public GameObject machineGun;
    public int money;
    public int zombieHitMoney;
    
    public float currentHealth;
    public float currentStamina;

    private Animator anim;
    private CharacterController controller;
    private float playerSpeed;
    private bool resting;
    private Vector3 pointToLook;
    private int activeChildIndex;

    // Start is called before the first frame update
    void Start()
    {
        activeChildIndex = 0;
        character = transform.GetChild(activeChildIndex).transform.gameObject;
        anim = transform.GetChild(activeChildIndex).gameObject.GetComponent<Animator>();
        controller = transform.GetChild(activeChildIndex).gameObject.GetComponent<CharacterController>();
        playerSpeed = moveSpeed;
        currentHealth = maxHealth; 
        currentStamina = maxStamina;
        money = 0;
        resting = false;
        
    }

    public void GetActivePlayerPrefab()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf == true)
            {
                activeChildIndex = i;
            }
        }
    }

    public void ChangePrefab(string gun)
    {
        switch (gun)
        {
            case "Rifle":
                rifle.transform.position = character.transform.position;
                character.SetActive(false);
                rifle.SetActive(true);
                gameUI.GetActivePlayerPrefab();
                follow.GetActivePlayerPrefab();
                GetActivePlayerPrefab();

                character = transform.GetChild(activeChildIndex).transform.gameObject;
                anim = transform.GetChild(activeChildIndex).gameObject.GetComponent<Animator>();
                controller = transform.GetChild(activeChildIndex).gameObject.GetComponent<CharacterController>();
                break;
            case "MachineGun":
                machineGun.transform.position = character.transform.position;
                character.SetActive(false);
                machineGun.SetActive(true);
                gameUI.GetActivePlayerPrefab();
                follow.GetActivePlayerPrefab();
                GetActivePlayerPrefab();

                character = transform.GetChild(activeChildIndex).transform.gameObject;
                anim = transform.GetChild(activeChildIndex).gameObject.GetComponent<Animator>();
                controller = transform.GetChild(activeChildIndex).gameObject.GetComponent<CharacterController>();
                break;
            default:
                break;
        }
            
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
            RegenStamina();
        }
        //Idle and Running animations
        if (!Input.anyKey)
        {
            anim.ResetTrigger("Run");
            anim.SetTrigger("Idle");
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
            transform.GetChild(activeChildIndex).transform.LookAt(new Vector3(pointToLook.x, transform.GetChild(activeChildIndex).transform.position.y, pointToLook.z));
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

    void RegenStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaDrain * Time.deltaTime;
        }
    }

    public void IncreaseMoney()
    {
        money += zombieHitMoney;
    }


    public void DecreaseHealth(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("GameOverScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            Movement();
            PlayerRotation();
        }
    }
}
