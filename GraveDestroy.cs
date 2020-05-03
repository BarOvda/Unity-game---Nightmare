using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GraveDestroy : MonoBehaviour
{
   public MeshRenderer geaveMeshRenderer;
    public AudioClip graveBrakeSound;
    public FirstPersonController fpcs;
    
    public Transform sphareCast;
    public LayerMask gravestonesLayer;
    private LevelTwo1 levelTwo;
    // Start is called before the first frame update
    void Start()
    {
        GameObject HUD = GameObject.Find("HUD");
        levelTwo= HUD.GetComponent<LevelTwo1>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            GraveAttack();
    }
    public void GraveAttack()
    {
       
        RaycastHit rHit;
        if (Physics.SphereCast(sphareCast.position, 0.2f, sphareCast.TransformDirection(Vector3.forward), out rHit, gravestonesLayer))
        {
            if (rHit.transform.GetComponent<MeshRenderer>().enabled == true&&rHit.transform.tag=="Grave")
            {
                AudioSource.PlayClipAtPoint(graveBrakeSound, transform.position); 
                rHit.transform.GetComponent<MeshRenderer>().enabled = false;
                levelTwo.GraveHit();
            }
        }
    }
}
