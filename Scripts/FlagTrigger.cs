using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrigger : MonoBehaviour
{
 
    private LevelFour levelFour;
    // Start is called before the first frame update
    void Start()
    {
        GameObject HUD = GameObject.Find("HUD");
        levelFour = HUD.GetComponent<LevelFour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter()
    {
        levelFour.LevelComplete();

    }
   
}
