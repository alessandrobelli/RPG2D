using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character {

    

    public Transform target;
    private Vector2[] possibleDirections =
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    bool moving = false;

    public override void Start()
    {
        base.Start();
        health.Initialize(startingHealth);

        damage = 20f;
        speed = 150f;

    }

    // Update is called once per frame
    protected override void Update () {

        if (target != null) moving = true;
        bool isTimeToWalk = (Random.Range(0, 400.0f) < 1 && !moving);
        
        if (isTimeToWalk)
        {
         transform.Translate(possibleDirections[Random.Range(0, 4)]);
            
        }


        counterAttack();

    }

    /// <summary>
    /// At first just go in the exact opposite direction of the attacker
    /// </summary>
    void counterAttack()
    {
        if (target == null)
        {
            rb2D.velocity = Vector2.zero;
            return;
        }
    
        Vector2 direction = target.position - transform.position;
        transform.LookAt(target.position);
        float move = speed * Time.deltaTime;
        

        if(Vector2.Distance(transform.position,target.position) > 1f)
        {

            rb2D.velocity = direction * move;
            moving = true;
        }
        else
        {
            rb2D.velocity = Vector2.zero;
            moving = false;
        }
    }



}
