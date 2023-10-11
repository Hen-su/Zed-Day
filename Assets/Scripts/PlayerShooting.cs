using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameManager Manager;
    public GameObject bullet;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
        bullet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet);
            bullet.SetActive(true);
        }
    }
}
