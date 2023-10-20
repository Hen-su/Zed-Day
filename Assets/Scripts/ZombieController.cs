using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float moveSpeed;
    public float attackDist;
    public float damage;
    public float attackDelay;
    public float destroyDelay;
    public Slider healthbar;

    private Animator anim;
    private CharacterController controller;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    private void UpdateHealth()
    {
        if (currentHealth > 0)
        {
            healthbar.value = currentHealth / maxHealth;
        }
        else
        {
            healthbar.value = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit by Bullet");
            DecreaseHealth();
            Debug.Log(currentHealth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            attack();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            attack();
        }
    }

    void DecreaseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth = currentHealth - damage;
            UpdateHealth();
        }
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            transform.position = transform.position;
            anim.Play("Z_death_A");
            Destroy(gameObject, destroyDelay);
        }
    }

    void attack()
    {

        anim.SetTrigger("Attack");
    }

    void Movement()
    {
        Vector3 targetVector = target.transform.position - transform.position;
        targetVector.Normalize();
        transform.LookAt(target.transform);
        controller.Move( targetVector * moveSpeed * Time.deltaTime);
        anim.SetTrigger("Walk");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            target = GameObject.FindWithTag("Soldier");
            Debug.Log(target.transform.position);
            Movement();
        }
        Death();
    }
}
