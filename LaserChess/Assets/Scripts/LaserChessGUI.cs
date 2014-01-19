 using UnityEngine;
using System.Collections;

public class LaserChessGUI : MonoBehaviour {

    


    void OnGUI()
    {
        GUI.color = Color.cyan;
        GUI.Box(new Rect((Screen.width/2 - 68), (Screen.height/2 - 25) , 160 , 140 ) , "Laser Chess");
        GUI.color = Color.red;
        if (GUI.Button (new Rect (Screen.width/2 - 50, Screen.height/2 + 10, 115, 60) , "Click here to play"))
        {
            Application.LoadLevel(1); ;
        }
    }

    




}
