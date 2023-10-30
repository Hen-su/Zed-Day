using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameUI gameUI;
    public string playerPrefab;
    public GameObject bullet;
    public GameObject barrelEnd;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public float pistolSpeed;
    public float rifleSpeed;
    public float machinegunSpeed;
    public float fireRate;
    private float nextFire = 0f;
    bool isReloading;
    private int pistolAmmo = 7;
    private int rifleAmmo = 10;
    private int machinegunAmmo = 40;
    private float speed;

    public AudioSource audioSource;
    public AudioClip shot;
    public AudioClip reload;
    // Start is called before the first frame update
    void Start()
    {
        isReloading = false;

        playerPrefab = transform.parent.tag;
        if (playerPrefab == "RiflePlayer")
        {
            maxAmmo = rifleAmmo;
            speed = rifleSpeed;
        }else if (playerPrefab == "PistolPlayer")
        {
            maxAmmo = pistolAmmo;
            speed = pistolSpeed;
        }
        else if (playerPrefab == "MachineGunPlayer")
        {
            maxAmmo = machinegunAmmo;
            speed = machinegunSpeed;
        }
        currentAmmo = maxAmmo;
    }

    public void SingleShot()
    {
        if (isReloading)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(shot);
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

    public void RapidFire()
    {
        if (isReloading) { return; }
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            audioSource.PlayOneShot(shot);
            GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBullet.transform.rotation = Quaternion.identity;
            instBulletRigidbody.AddForce(transform.forward * speed);
            currentAmmo--;
            nextFire = Time.time + 1f / fireRate;
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        
    }

    void ManualReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine (Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reload);
        yield return new WaitForSeconds(reloadTime);
        audioSource.PlayOneShot(reload);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        SingleShot();
        if (playerPrefab == "MachineGunPlayer")
        {
            RapidFire();
        }
        ManualReload();
    }
}
