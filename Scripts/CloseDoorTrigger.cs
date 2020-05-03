using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class CloseDoorTrigger : MonoBehaviour
{
    public Animator leftDoor;
    public Animator rightDoor;
    public AudioClip closeSound;
    bool firsTime = false;
    public void OnTriggerEnter()
    {
        leftDoor.SetBool("Close", true);
        rightDoor.SetBool("Close", true);
        if(!firsTime)
        AudioSource.PlayClipAtPoint(closeSound, transform.position);
        firsTime = true;
    }
}
