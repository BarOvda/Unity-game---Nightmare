using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCheckpoint : MonoBehaviour
{    
 
    private Level3 levelThree;
    // Start is called before the first frame update
    void Start()
    {
        GameObject HUD = GameObject.Find("HUD");
        levelThree = HUD.GetComponent<Level3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter()
    {
        print("incircle!");
        levelThree.circleFound();

    }
    void OnTriggerExit()
    {

        
    }
}
