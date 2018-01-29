using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {

    private Vector2 enemyDirection;

    public override void Start()
    {
        health.Initialize(100);
        damage = 20f;
        speed = 3f;
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


        private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collision: "+collision.tag);
        Vector3 dirFromAtoB = (collision.transform.position - transform.position).normalized;
        if (direction != Vector2.zero) enemyDirection = direction;
        float dotProd = Vector3.Dot(dirFromAtoB, enemyDirection);
        


        if ((collision.tag == "Player" || collision.tag == "Enemy" ) && !IsMoving)
        {
                  animator.SetLayerWeight(2, 1);
                if (Input.GetKeyDown(KeyCode.X))
                {

                    animator.SetBool(combatAnimation, true);
                    animationTime = Time.time;
                    currentTarget = collision.transform.gameObject.GetComponent<Character>();
                    attack(currentTarget, damage);
                    Debug.Log("attack " + collision.transform.name);

                }
                else if ((Time.time - animationTime) >= fadeTime)
                {

                    animationTime = 0;
                    animator.SetBool(combatAnimation, false);

                }



        }
        else
        {
            animator.SetLayerWeight(2, 0);
            animator.SetBool(combatAnimation, false);
      
        }

    }


}
