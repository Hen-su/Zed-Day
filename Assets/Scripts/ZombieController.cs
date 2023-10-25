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
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        isAttacking = false;
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.tag);
        if (hit.gameObject.tag == "Soldier")
        {
            Debug.Log("attacking");
            Attack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit by Bullet");
            DecreaseHealth();
            Debug.Log(currentHealth);
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

    void Attack()
    {
        isAttacking = true;
        anim.ResetTrigger("Walk");
        anim.SetTrigger("Attack");
    }

    public void StopAttack()
    {
        isAttacking = false;
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
            if (isAttacking == false)
            {
                target = GameObject.FindWithTag("Soldier");
                //Debug.Log(target.transform.position);
                Movement();
            }
            
        }
        Death();
    }
}
