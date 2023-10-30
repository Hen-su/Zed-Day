using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    public PlayerController playercontroller;
    public float maxHealth;
    private float currentHealth;
    public float moveSpeed;
    public float attackDist;
    public float damage;
    public float attackDelay;
    public float destroyDelay;
    public Slider healthbar;
    public float bulletDamage;
    public float meleeDamage;
    public NavMeshAgent navMeshAgent;

    public List<CharacterController> characterControllerList;
    List<Transform> currentNPCList;

    public AudioClip hitAudio;
    public AudioClip zombieDeath;
    public AudioClip zombieSpawn;
    public AudioSource audioSource;

    private Animator anim;
    private CharacterController controller;
    private GameObject target;
    private bool isAttacking;
    private int activeChildIndex;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        isAttacking = false;
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        UpdateEnemyList();
        audioSource.PlayOneShot(zombieSpawn);
    }

    void UpdateEnemyList()
    {
        currentNPCList = new List<Transform>();
        GameObject[] civilians = GameObject.FindGameObjectsWithTag("Civilian");
        foreach (GameObject g in civilians)
        {
            currentNPCList.Add(g.transform);
        }
        GameObject playerPrefab = playercontroller.transform.GetChild(activeChildIndex).gameObject;
        currentNPCList.Add(playerPrefab.transform);
    }

    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            if (t != null)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            
        }
        return tMin;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "CivilianWeapon")
        {
            DecreaseHealth();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Civilian")
        {
            Attack();
        }
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("Hit by Bullet");
            DecreaseHealth();
            playercontroller.IncreaseMoney();
            //Debug.Log(currentHealth);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.parent != null)
        {
            if (hit.transform.parent.tag == "Player")
            //Debug.Log("attacking");
            Attack();
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
            audioSource.PlayOneShot(zombieDeath);
            transform.position = transform.position;
            anim.Play("Z_death_A");
            controller.enabled = false;
            Destroy(gameObject, destroyDelay);
        }
    }

    void Attack()
    {
        audioSource.PlayOneShot(hitAudio);
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
        controller.Move(targetVector * moveSpeed * Time.deltaTime);
        //navMeshAgent.Move(targetVector * moveSpeed * Time.deltaTime);
        anim.SetTrigger("Walk");
    }

    // Update is called once per frame
    void Update()
    {
        target = GetClosestEnemy(currentNPCList).gameObject;
        if (currentHealth > 0)
        {
            if (isAttacking == false)
            {
                
                //Debug.Log(target.transform.position);
                Movement();
            }
            
        }
        Death();
        UpdateEnemyList();
    }
}
