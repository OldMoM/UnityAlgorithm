using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITest : MonoBehaviour
{
    private void OnGUI()
    {
        //Make a background box
        GUI.Box(new Rect(10, 10, 120, 90),"Loader Menu");

        if (GUI.Button(new Rect(20,40,100,20),"Level 1"))
        {
            Debug.Log("Load Level 1");
        }

        if(GUI.Button(new Rect(20, 70, 100, 20), "Level 2"))
        {
            Debug.Log("Load Level 2");
        }


    }
}
