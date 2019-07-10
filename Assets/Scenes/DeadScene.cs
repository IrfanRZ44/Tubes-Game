using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScene : MonoBehaviour
{
    public GUISkin gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void dead()
    {
        Application.LoadLevel("Lvl2");
    }

    private void OnGUI()
    {
            GUI.skin = gameOver;
            if (GUI.Button(new Rect((Screen.width) / 2, (Screen.height) / 2 - 50, 200, 200), ""))
            {
                Invoke("dead", 0.2f);
            }
    }
}
