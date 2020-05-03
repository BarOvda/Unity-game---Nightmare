using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTrigger : MonoBehaviour
{
    public Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = this.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorAnimator.SetBool("isOpen", true);
            print("open");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
