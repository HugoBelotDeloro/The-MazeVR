using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivate : MonoBehaviour
{
    public bool open;

    private PlayerCursor pc;

    public GameObject player;

    public GameObject trigger;

    private bool sleep=false;
    
    private void Start()
    {
        
    }

    void Update()
    {
        player = GameObject.Find("Player(Clone)");
        pc = player.GetComponent<PlayerCursor>();
        if (pc.LookingGameObject == trigger)
        {
            if (Input.GetAxis("Action") > 0)
            {
                if (!sleep)
                {
                    if (!open)
                    {
                        transform.GetChild(2).transform.position += transform.forward * 0.75f;
                        transform.GetChild(1).transform.position -= transform.forward * 0.75f;
                        open = true;
                    }
                    else
                    {
                        transform.GetChild(2).transform.position -= transform.forward * 0.75f;
                        transform.GetChild(1).transform.position += transform.forward * 0.75f;
                        open = false;
                    }
                StartCoroutine(Wait());
                }
            }
        }
    }

    IEnumerator Wait()
    {
        sleep = true;
        yield return new WaitForSeconds(0.25f);
        sleep = false;
    }
}
