using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public static bool night;

    [Tooltip("number of minutes per second that pass, try 60")]
    public float timeScale;

    public float angleThisFrame;

    // Use this for initialization
    void Start () {
        night = false;
	}


    // Update is called once per frame
    void Update()
    {
         angleThisFrame = Time.deltaTime / 360 * timeScale;


        transform.RotateAround(transform.position, Vector2.right, angleThisFrame);
       

    }
}
