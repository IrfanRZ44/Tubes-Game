using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lv_2_Controller : MonoBehaviour
{
    public AudioSource attackSound;
    public AudioClip attackClip;
    public AudioSource jumpSound;
    public AudioClip jumpClip;
    public GUISkin mySkin;
    public GUISkin mySkin2;
    public GUISkin mySkin3;
    public GUISkin skinFinish;
    public GUISkin skinDead;

    float speed = 4;
    float rotSpeed = 80;
    float rot = 0;
    float gravity = 8;
    bool valueChest = false;
    bool valueChest2 = false;

    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator anim;

    bool attack = false;
    int value = 0;
    int value1 = 0;
    int getItem = 0;
    bool endGame = false;
    bool dead = false;

    void Start()
    {
        attackSound.clip = attackClip;
        jumpSound.clip = jumpClip;
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

                if (transform.position.y < -1)
                {
                    dead = true;
               }
    }

    void cekCharacterPosition()
    {
        if(transform.position.x > -14 && transform.position.x < - 8 && transform.position.z < 27 && transform.position.z > 23 && transform.position.y > 5)
        {
            if (getItem == 2)
            {
                endGame = true;
            }
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
                        jumpSound.Play();
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
                        jumpSound.Play();
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
                    jumpSound.Play();
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
                    jumpSound.Play();
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                    moveDir = new Vector3(0, 0, 0);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                //walkSound.Stop();
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                //walkSound.Play();
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
        attackSound.Play();
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
            if (getItem == 0)
            {
                getItem = 1;
            }
            else
            {
                getItem = 2;
            }
            other.gameObject.transform.position = new Vector3(-20, -50, -20);
        }
    }

    void Restart()
    {
        Application.LoadLevel("SplashScreen");
    }

    void DeadScene()
    {
        Application.LoadLevel("Lvl2");
    }

    private void OnGUI()
    {
       
        if (getItem == 0)
        {
            GUI.skin = mySkin;
            valueChest = GUI.Toggle(new Rect(20, 20, 40, 40), valueChest, "");
            valueChest2 = GUI.Toggle(new Rect(70, 20, 40, 40), valueChest2, "");
        }
        else if (getItem == 1)
        {
            GUI.skin = mySkin3;
            valueChest = GUI.Toggle(new Rect(20, 20, 120, 80), valueChest, "");
        }
        else if (getItem == 2)
        {
            GUI.skin = mySkin2;
            valueChest = GUI.Toggle(new Rect(10, 10, 80, 80), valueChest, "");
            valueChest2 = GUI.Toggle(new Rect(95, 10, 80, 80), valueChest2, "");
        }

        if (endGame)
        {
            GUI.skin = skinFinish;
            if (GUI.Button(new Rect((Screen.width) / 2, (Screen.height) / 2 - 50, 200, 170), ""))
            {
                Invoke("Restart", 0.2f);
            }
        }

        if (dead)
        {
            GUI.skin = skinDead;
            if (GUI.Button(new Rect((Screen.width) / 2, (Screen.height) / 2 - 50, 200, 200), ""))
            {
                Invoke("DeadScene", 0.2f);
                dead = false;
            }
        }

    }
}
