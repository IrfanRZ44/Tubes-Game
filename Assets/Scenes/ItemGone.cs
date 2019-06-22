using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGone : MonoBehaviour
{
    public GameObject game;

    void Start()
    {
    }

    void Update()
    {
        if((game.transform.position.z == transform.position.z) || (game.transform.position.y == transform.position.y) 
            || (game.transform.position.x == transform.position.x)
            )
        {
            transform.position = new Vector3(-20, -20, -20);
        }
    }
}
