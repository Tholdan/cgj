using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingColorPlatform : MonoBehaviour {

    //PUBLIC
    public Renderer render;
    public Color[] colorArray;

    //PRIVATE
    private int currentColor;

    void Start()
    {
        currentColor = 0;
        render.material.color = colorArray[currentColor];
    }

    void OnCollisionEnter(Collision collider)
    {
        currentColor = (currentColor + 1) % colorArray.Length;
        render.material.color = colorArray[currentColor];
    }
}
