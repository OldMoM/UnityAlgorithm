using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILayoutTest : MonoBehaviour
{
    private float group_width = 150;
    private float group_height = 100;

    [SerializeField]
    private float mySlider = 1.0f;

    private void OnGUI()
    {
        GUILayout.Button("Automatic Layout Button");
        GUILayout.Button("I am an Automatic Layout Button2-----------sjfhskdjf");

        GUI.BeginGroup(new Rect(Screen.width / 2 - group_width / 2, Screen.height / 2 - group_height / 2, group_width, group_height));
        GUI.Box(new Rect(0, 0, 100, 100), "Group is here");
        GUI.Button(new Rect(5,25,90,20),"Click Me");
        GUI.EndGroup();

        GUILayout.Button("Not inside Area");
        GUILayout.BeginArea(new Rect(100, 100, 300, 400));
        GUILayout.Button("Inside Area 1");
        GUILayout.Button("Inside Area 2");
        GUILayout.Button("Inside Area 3");
        GUILayout.EndArea();

        mySlider = LableSlider(new Rect(10, 200, 100, 20), mySlider, 5.0f, "Label text here");
    }

    private float LableSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText)
    {
        GUI.Label(screenRect,labelText);
        screenRect.x += screenRect.width;
        sliderValue = GUI.HorizontalSlider(screenRect, sliderValue, 0, sliderMaxValue);
        return sliderValue;
    }
}
