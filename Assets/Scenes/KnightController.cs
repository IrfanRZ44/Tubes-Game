using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnightController : MonoBehaviour
{
    public GameObject gameItem;
    public TextMeshPro text;
    public TextMeshPro textFinish;
    
    float speed = 4;
    float rotSpeed = 80;
    float rot = 0;
    float gravity = 8;

    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator anim;

    bool attack = false;
    int value = 0;
    int value1 = 0;
    bool getItem = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        InvokeRepeating("repeat", 1, 3);
    }

    void repeat()
    {
        attack = false;
        value = value + 1;
    }

    void Update()
    {
        Movement();
        GetInput();
        cekCharacterPosition();
    }

    void cekCharacterPosition()
    {
        if(transform.position.x > -6 && transform.position.x < 6 && transform.position.z < -2 && transform.position.z > -9)
        {
            textFinish.text = "Find a treasure chest and bring it back here";
            if (getItem)
            {
                textFinish.text = "Finish";
            }
        }
        else
        {
            textFinish.text = "";
        }

        if (transform.position.y > 4 && transform.position.y < 7
            && transform.position.x > 33 && transform.position.x < 40
            && transform.position.z < -41)
        {
            if (getItem)
            {
                text.text = "Get back to the start zone";
            }
            else
            {
                text.text = "Hit This Chest";
            }
        }
        else
        {
            text.text = "";
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (anim.GetBool("attacking") == true)
                        {
                            return;
                        }
                        else if (anim.GetBool("attacking") == false)
                        {
                            anim.SetBool("running", true);
                            anim.SetInteger("condition", 1);
                            moveDir = new Vector3(0, 2, 1);
                            moveDir *= speed;
                            moveDir = transform.TransformDirection(moveDir);
                        }
                    }
                    else
                    {
                        anim.SetBool("running", true);
                        anim.SetInteger("condition", 1);
                        moveDir = new Vector3(0, 0, 1);
                        moveDir *= speed;
                        moveDir = transform.TransformDirection(moveDir);
                    }
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        anim.SetBool("running", false);
                        anim.SetInteger("condition", 0);
                        moveDir = new Vector3(0, 0, 0);
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (anim.GetBool("attacking") == true)
                    {
                        return;
                    }
                    else if (anim.GetBool("attacking") == false)
                    {
                        anim.SetBool("running", true);
                        anim.SetInteger("condition", 1);
                        moveDir = new Vector3(0, 2, 0);
                        moveDir *= speed;
                        moveDir = transform.TransformDirection(moveDir);
                    }
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                    moveDir = new Vector3(0, 0, 0);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, -1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }            
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButton(0))
            {
                if (anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }
                if (anim.GetBool("running") == false)
                {
                    Attacking();
                }
            }
        }
    }

    void Attacking()
    {
        attack = true;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(0.6f);  

        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attack)
        {
            gameItem.transform.position = new Vector3(-20, -20, -20);
            getItem = true;
        }
        
    }
}
