using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public string WhatScene;
    public GUISkin mySkin;
    bool value = true;

    void Start()
    {
        
    }

    void LoadingScene()
    {
        Application.LoadLevel(WhatScene);
    }

    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.skin = mySkin;
        //value = GUI.Toggle(new Rect(Screen.width - 70, 20, 60, 60), value, "");
        if (GUI.Button(new Rect((Screen.width) / 2, (Screen.height) / 2 - 50, 300, 300), ""))
        {
            Invoke("LoadingScene", 0.2f);
        }
        /* if (alpha > -1)
         {
             alpha -= fadeSpeed * Time.deltaTime;
             Color temp = GUI.color;
             temp.a = alpha;
             GUI.color = temp;
             GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
         }
         else
         {
             Invoke("LoadingScene", DelayTime);
         }
         */
    }
}
