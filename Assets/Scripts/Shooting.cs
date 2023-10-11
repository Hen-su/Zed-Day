using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject barrelEnd;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public float speed;
    bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        isReloading = false;
        currentAmmo = maxAmmo;
    }


    private void Shoot()
    {
        if (isReloading)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBullet.transform.rotation = Quaternion.identity;
            instBulletRigidbody.AddForce(transform.forward * speed);
            currentAmmo--;
        }
        
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
