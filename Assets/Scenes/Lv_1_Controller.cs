using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lv_1_Controller : MonoBehaviour{
    public GameObject gameItem;
    public GameObject gameItemObject;
    public TextMeshPro text;
    public TextMeshPro textFinish;
    public AudioSource attackSound;
    public AudioClip attackClip;
    public AudioSource jumpSound;
    public AudioClip jumpClip;
    public GUISkin mySkin;
    public GUISkin mySkin2;
    public GUISkin mySkin3;

    float speed = 4;
    float rotSpeed = 80;
    float rot = 0;
    float gravity = 8;
    bool valueChest = false;
    bool dead = false;

    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator anim;

    bool attack = false;
    int value = 0;
    int value1 = 0;
    bool getItem = false;
    bool nextLvl = false;

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
        if(transform.position.y < 0)
        {
            dead = true;
        }
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
                nextLvl = true;
                //getItem = false;
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
            gameItem.transform.position = new Vector3(-20, -20, -20);
            gameItemObject.transform.position = new Vector3(-20, -20, -20);
            getItem = true;
        }
    }

    void DeadScene()
    {
        Application.LoadLevel("Lvl1");
    }

    void nextLevel()
    {
        Application.LoadLevel("Lvl2");
    }

    private void OnGUI()
    {
        if (getItem)
        {
            GUI.skin = mySkin2;
            valueChest = GUI.Toggle(new Rect(10, 10, 80, 80), valueChest, "");
        }
        else
        {
            GUI.skin = mySkin;
            valueChest = GUI.Toggle(new Rect(20, 20, 40, 40), valueChest, "");
        }

        if (dead)
        {
            if (GUI.Button(new Rect((Screen.width) / 2, (Screen.height) / 2 - 50, 200, 200), ""))
            {
                Invoke("DeadScene", 0.2f);
                dead = false;
            }
        }
        if (nextLvl)
        {
            GUI.skin = mySkin3;

            if (GUI.Button(new Rect((Screen.width) / 2, (Screen.height) / 2 - 50, 200, 180), ""))
            {
                Invoke("nextLevel", 0.2f);
                dead = false;
            }
        }
    }
}
