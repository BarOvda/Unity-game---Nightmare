using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    public void DamageEventN()
    {
        player.MacheteAttack();
    }
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
