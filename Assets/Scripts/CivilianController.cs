using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CivilianFollow : MonoBehaviour
{
    public GameManager gameManager;
    public float maxHealth;
    public float moveSpeed;
    public float offset;
    public float deathDelay;
    public Slider healthbar;
    public float zombieDamage;
    public float bulletDamage;

    public AudioClip hitAudio;
    public AudioSource audioSource;

    private GameObject target;
    private CharacterController characterController;
    private Animator animator;
    private float currentHealth;
    private GameObject enemyTarget;
    private bool hasTarget;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
        hasTarget = false;
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

    public void DecreaseHealth(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            UpdateHealth();
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
            transform.position = transform.position;
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        characterController.enabled = false;
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            if (other.transform.parent.tag == "Player")
            {
                target = other.gameObject;
                if (!gameManager.followingList.Contains(gameObject.GetComponent<CharacterController>()))
                {
                    gameManager.followingList.Add(gameObject.GetComponent<CharacterController>());
                }
                //Debug.Log(target.name);
            }
        }

        if (other.tag == "ZombieArms")
        {
            //Debug.Log("Hit by zombie");
            DecreaseHealth(zombieDamage);
        }

        if (other.transform.tag == "Zombie")
        {
            enemyTarget = other.gameObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("Hit by zombie");
            DecreaseHealth(bulletDamage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Zombie")
        {
            Debug.Log("Following Zombie");
            enemyTarget = other.gameObject;
            hasTarget = true;
        }
    }

    void CheckEnemyTarget()
    {
        if (enemyTarget != null && enemyTarget.activeSelf)
        {
            hasTarget = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Zombie")
        {
            Attack();
        }
    }

    void Attack()
    {
        if (enemyTarget.gameObject.GetComponent<CharacterController>() != null)
        {
            audioSource.PlayOneShot(hitAudio);
            animator.SetTrigger("Attack");
        }
    }

    void FollowEnemy()
    {
        Vector3 targetVector = enemyTarget.transform.position - transform.position;
        targetVector.Normalize();
        transform.LookAt(enemyTarget.transform);
        characterController.Move(targetVector * moveSpeed * Time.deltaTime);
        animator.SetTrigger("Run");
    }

    void FollowPlayer()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            Vector3 targetPosition = target.transform.position - offset * direction.normalized;
            transform.LookAt(target.transform);
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            if (distanceToTarget > 0.5f)
            {
                characterController.Move((targetPosition - transform.position) * moveSpeed * Time.deltaTime);
                animator.SetTrigger("Run");
                
            }
            else
            {
                animator.ResetTrigger("Run");
                animator.SetTrigger("Idle");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget == false)
        {
            FollowPlayer();
        }
        else
        {
            FollowEnemy();
        }
        //Debug.Log("Health = " + currentHealth);
        Debug.Log(hasTarget);
        CheckEnemyTarget();
    }
}
