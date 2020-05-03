using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;



public class AIZombie : MonoBehaviour
{
    public bool isDead = false;
    public int health = 50;
    public float wanderRadios = 10f;
    public FirstPersonController fpsc;
    public float wanderSpeed = 1.25f;
    public float chaseSpeed = 5f;
    private Vector3 wanderPoint;
    private Collider[] cBody;
    private Rigidbody[] rBody;
    private NavMeshAgent agent;
    private bool isAware = false;
    private bool isAttacking = false;
    private float viewDistance = 8f;
    private float attackDistance = 1f;
    private Animator animator;
    public MeshRenderer meshRenderer;
    public AudioClip wanderingSound;
    public AudioClip ChaseSound;
    public int attackDamage = 30;
    public Transform sphereCastSpawn;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        wanderPoint = RandomWanderingPoint();
        animator = GetComponentInChildren<Animator>();
        cBody = GetComponentsInChildren<Collider>();
        rBody = GetComponentsInChildren<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        //ac = GetComponent<AudioClip>();
        
        foreach (Collider col in cBody)
        {
            if(!col.CompareTag("Zombie"))
            col.enabled = false;
        }
        foreach(Rigidbody rb in rBody)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (health <= 0)
        {
            Die();
            
            return;
        }
        searchForPlayer();
        if (isAttacking)
        {
            Attack();
            animator.SetBool("Attack", true);
            animator.SetBool("Aware", false);
        }
        else if (isAware)
        {
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed;
           
            agent.SetDestination(fpsc.transform.position);
            
        }
        else
        {
            agent.speed = wanderSpeed;
            agent.destination = fpsc.transform.position;
            animator.SetBool("Attack", false);
            animator.SetBool("Aware", false);
            wander();
  
        }
    }
    public Vector3 RandomWanderingPoint()
    {
        //TODO

        AudioSource.PlayClipAtPoint(wanderingSound, transform.position);
            Vector3 randomPoint = new Vector3();
        randomPoint=(Random.insideUnitSphere*wanderRadios)+transform.position;
        NavMeshHit nHit;
       
        if (NavMesh.SamplePosition(randomPoint, out nHit, wanderRadios, NavMesh.AllAreas))
        {
            print(nHit.position);
            return new Vector3(nHit.position.x, transform.position.y, nHit.position.z);
            
        }
        
        return Vector3.zero;
    }
    public void wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 0.5f)
        {
            wanderPoint = RandomWanderingPoint();
        }
        else { 
        
        agent.SetDestination(wanderPoint);
            }
        
    }
    public void searchForPlayer()
    {
        if (Vector3.Distance(fpsc.transform.position, transform.position) < attackDistance)
        {
            isAttacking = true;
            isAware = false;
        }
        else if (Vector3.Distance(fpsc.transform.position, transform.position) < viewDistance)
        {
            if (isAware == false)
            {
                AudioSource.PlayClipAtPoint(ChaseSound, transform.position);
                isAware = true;
            }
            isAttacking = false;
            
        }
        else
        {  
            isAware = false;
            isAttacking = false;
        }
    }
    public void OnHit(int damage)
    {
        health -= damage;
                
    }
    public void Die()
    {
        agent.speed = 0;
        animator.enabled = false;
        
        foreach(Collider col in cBody)
        {
            col.enabled = true;
             
        }
        foreach(Rigidbody rb in rBody)
        {
            rb.isKinematic = false;
        }
        isDead = true;
    }
    public void Attack()
    {
        
        RaycastHit rHit;
        if (Physics.SphereCast(sphereCastSpawn.position, 0.5f, sphereCastSpawn.TransformDirection(Vector3.forward), out rHit, playerLayer))
        {
            rHit.transform.GetComponent<Player>().OnHit(attackDamage);
        }
    }
}
