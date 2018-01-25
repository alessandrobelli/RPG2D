using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {
    

    public override void Start()
    {
        health.Initialize(100);
        damage = 20f;
        speed = 7f;
        base.Start();

    }
    

    // Update is called once per frame
    protected override void Update () {
        GetInput();
       
        base.Update();
	}

    public void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
           
            direction = Vector2.up;
            facing = CURRENTLY_FACING.UP;
        }

        if (Input.GetKey(KeyCode.D))
        {
        
            direction = Vector2.right;
            facing = CURRENTLY_FACING.RIGHT;
        }


        if (Input.GetKey(KeyCode.S))
        {

            facing = CURRENTLY_FACING.DOWN;
                 direction = Vector2.down;
        }


        if (Input.GetKey(KeyCode.A))
        {
            facing = CURRENTLY_FACING.LEFT;
            direction = Vector2.left;
        }
        

    }



}
