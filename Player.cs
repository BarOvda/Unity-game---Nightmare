using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    private Animator machete;
    // Start is called before the first frame update
    public Transform sphereCastSpawn;
    public LayerMask zombieLayer;
    public int attackDamage = 40;
    public float hitDistance = 2f;
    private AudioSource audioSource;
    public AudioClip macheteAttack;
    public Slider healthBar;
    public int health = 300;
    void Start()
    {
        machete = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
            
            machete.SetTrigger("Attack");
        }
    }
    public int getHealth()
    {
        return this.health;
    }
    public void MacheteAttack()
    {
        
        audioSource.PlayOneShot(macheteAttack);
        
        RaycastHit rHit;
        if(Physics.SphereCast(sphereCastSpawn.position, 0.5f, sphereCastSpawn.TransformDirection(Vector3.forward),out rHit, zombieLayer))
        {
            AIZombie zombie = rHit.transform.GetComponent<AIZombie>();
            if (Vector3.Distance(transform.position, zombie.transform.position) < hitDistance)
                zombie.OnHit(attackDamage);
        }

    }
    public void OnHit(int damage)
    {
        health -= damage;
        healthBar.value = health;
    }
}
