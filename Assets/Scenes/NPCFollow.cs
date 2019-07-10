using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCFollow : MonoBehaviour
{
    public GameObject ThePlayer;
    public float TargetDistance;
    public float AllowedDistance = 100;
    public GameObject TheNPC;
    public float FollowSpeed;
    public RaycastHit Shot;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ThePlayer.transform);
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;
            if(TargetDistance <= AllowedDistance)
            {
                FollowSpeed = 0.04f;
                TheNPC.GetComponent<Animation>().Play("running_npc");
                transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, FollowSpeed);
            }
            else
            {
                FollowSpeed = 0;
                TheNPC.GetComponent<Animation>().Play("idle");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Invoke("Dead", 0.2f);
    }

    void Dead()
    {
        Application.LoadLevel("DeadScene");
    }
}
