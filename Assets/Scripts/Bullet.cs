using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager Manager;
    Quaternion shootDir;
    public float velocity;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocity;
    }
}
