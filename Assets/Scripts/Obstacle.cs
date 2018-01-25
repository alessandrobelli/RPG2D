using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IComparable<Obstacle> {

    /// <summary>
    /// Obstacle sprite renderer
    /// </summary>
    public SpriteRenderer mySpriteRenderer { get; set; }


    private Color defaultColor;
    private Color fadedColor;


    /// <summary>
    /// Sort layer function
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Obstacle other)
    {
        if(mySpriteRenderer.sortingOrder > other.mySpriteRenderer.sortingOrder)
        {
            return 1;
        }else if(mySpriteRenderer.sortingOrder < other.mySpriteRenderer.sortingOrder)
        {
            return -1;
        }
        return 0;
    }

 

	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = mySpriteRenderer.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.7f;
    }
	
    public void FadeOut()
    {
        mySpriteRenderer.color = fadedColor;
    }

    public void FadeIn()
    {
        mySpriteRenderer.color = defaultColor;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
