using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb2Trigger : MonoBehaviour
{
    public static int tombIndex = 1;
    public Light tombLight;
    private bool insideTobb = false;
    private LevelOne levelOne;
    // Start is called before the first frame update
    void Start()
    {
        GameObject HUD = GameObject.Find("HUD");
        levelOne = HUD.GetComponent<LevelOne>();
    }

    // Update is called once per frame
    void Update()
    {
        if (insideTobb && Input.GetButtonDown("Fire1"))
        {
            tombLight.intensity = 15;
            levelOne.TombFound(tombIndex);
        }
    }
    void OnTriggerEnter()
    {
        insideTobb = true;

    }
    void OnTriggerExit()
    {
       
        insideTobb = false;
    }
}
