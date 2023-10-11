using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
