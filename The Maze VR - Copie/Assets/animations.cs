using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animations : MonoBehaviour
{
    private Animator anim;
    public GameObject Player;
    private float attacktimer;
    private float rmattacktimer;
    public float attackdelay;
    public float rmattackdelay;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attacktimer = attackdelay;
        rmattacktimer = rmattackdelay;
    }

    // Update is called once per frame
    void Update()
    {
        attacktimer -= Time.deltaTime;
        rmattacktimer -= Time.deltaTime;
        if (attacktimer<=0&&Vector3.Distance(Player.transform.position, transform.position)<6)
        {
            anim.SetTrigger("Attack");
            attacktimer = attackdelay;
        }
        else if (rmattacktimer <= 0 && Vector3.Distance(Player.transform.position, transform.position) < 30)
        {
            anim.SetTrigger("RangeAttack");
            rmattacktimer = rmattackdelay;
        }
    }
}
