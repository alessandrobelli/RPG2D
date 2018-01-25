using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningArea : MonoBehaviour {


    Enemy enemy;

	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player") enemy.target = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") enemy.target = null;

    }
}
