using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject character;
    public FollowPlayer mainCamera;
    public float moveSpeed;
    public float sprintSpeed;
    public float smooth = 0.1f;

    private Animator anim;
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        mainCamera = FindObjectOfType<FollowPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player Rotation with WASD
        /*float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler( 0f, targetAngle, 0f);*/
        
        //Movement
        if (Input.GetKey("w"))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            anim.SetTrigger("Run");
            controller.Move(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("Left Shift"))
        {
            moveSpeed = sprintSpeed;
        }
        if (!Input.anyKey)
        {
            anim.ResetTrigger("Run");
            anim.Play("m_weapon_idle_A");
        }
        //Player Rotation with mouse
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylength;
        if (groundPlane.Raycast(cameraRay, out raylength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(raylength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
