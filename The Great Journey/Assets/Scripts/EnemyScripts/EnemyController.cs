using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    public ParticleSystem particles; //For Hell Knight's flames
    //public Transform[] waypoints;

    float nextTimeToFire = 0f; //Cooldown between attacks

    Enemy thisEnemy;
    Player player;
    Transform target;
    NavMeshAgent agent;
    Animator animator;
    bool isChasing = false;
    //bool patrol = false;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if(waypoints.Length > 1)
        {
            patrol = true;
            Patrol();
        }*/
        thisEnemy = GetComponent<Enemy>();
        target = PlayerManager.instance.player.transform;
        player = PlayerManager.instance.player.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisEnemy.isDead == false)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                //patrol = false;
                isChasing = true;
                agent.SetDestination(target.position);
                animator.SetBool("isChasing", true);
                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                    agent.isStopped = true;
                    animator.SetBool("inRange", true);
                    if (thisEnemy.type == "Knight")
                    {
                        KnightAttack(2f);
                    }
                    else if (thisEnemy.type == "Zombie")
                    {
                        ZombieAttack(2f);
                    }
                    //animator.SetBool("inRange", false);
                    //agent.isStopped = false;
                }
                else
                {
                    agent.isStopped = false;
                    animator.SetBool("inRange", false);
                    animator.SetBool("isChasing", true);
                }
            }
            else if (isChasing == true)
            {
                agent.SetDestination(target.position);
                animator.SetBool("isChasing", true);
            }
        }
        else if(thisEnemy.isDead == true)
        {
            agent.enabled = false;
            animator.SetBool("isDead", true);
        }
    }

    void KnightAttack(float fireRate)
    {
        if(Time.time >= nextTimeToFire)
        {
            particles.Play();
            player.TakeDamage(thisEnemy.damage);
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    void ZombieAttack(float fireRate)
    {
        if (Time.time >= nextTimeToFire)
        {
            //particles.Play();
            player.TakeDamage(thisEnemy.damage);
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }
    /*
    void Patrol()
    {
        if(patrol == true)
        {
            Random rand = new Random();
            int r = Random.Range(0, waypoints.Length);
        }
    }*/

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
