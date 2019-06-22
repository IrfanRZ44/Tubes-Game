using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject game;
    public float speed = 20f;

    public float jumpSpeed = 50.0f;
    public float gravity = 5.0f;
    private bool isJump = false;
    private int doubleJump = 0;
    private float maxJump = 3.0f;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey("w"))
        {
            game.transform.position = new Vector3(game.transform.position.x, game.transform.position.y, game.transform.position.z + speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            game.transform.position = new Vector3(game.transform.position.x, game.transform.position.y, game.transform.position.z - speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            game.transform.position = new Vector3(game.transform.position.x + speed * Time.deltaTime, game.transform.position.y, game.transform.position.z);
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey("a"))
        {
            game.transform.position = new Vector3(game.transform.position.x - speed * Time.deltaTime, game.transform.position.y, game.transform.position.z);
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown("space") && game.transform.position.y < maxJump && doubleJump < 2)
        {
            game.transform.position = new Vector3(game.transform.position.x, game.transform.position.y + jumpSpeed * Time.deltaTime, game.transform.position.z);
            isJump = true;
            doubleJump++;
        }
    }

    void FixedUpdate()
    {
        if (isJump && game.transform.position.y > 0)
        {
            game.transform.position = new Vector3(game.transform.position.x, game.transform.position.y - gravity * Time.deltaTime, game.transform.position.z);
        }

        if(game.transform.position.y <= 0)
        {
            isJump = false;
            doubleJump = 0;
        }

    }
}
