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
    Player player;

    public float distanceToPlayer { get; private set; }
    public bool playerInRange { get; private set; }

    private float angle;
    private float warningArea = 2f;

    public override void Start()
    {
        base.Start();
        health.Initialize(startingHealth);

        damage = 20f;
        speed = 30f;
        player = FindObjectOfType<Player>();
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
        stayAway();
    }

    void stayAway()
    {
        distanceToPlayer = Vector3.Distance((Vector2)transform.position, (Vector2)player.transform.position);

        angle = Vector2.Angle(transform.up, player.transform.position - transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (direction.magnitude < 0.6)
        {
            float move = (speed * 10) * Time.deltaTime;
            moving = true;
            rb2D.velocity = -direction * move;
        }
    }

    /// <summary>
    /// At first just go in the exact opposite direction of the attacker
    /// </summary>
    void counterAttack()
    {

        distanceToPlayer = Vector3.Distance((Vector2)transform.position, (Vector2)player.transform.position);

        angle = Vector2.Angle(transform.up, player.transform.position - transform.position);
        Vector2 direction = player.transform.position - transform.position;
         rb2D.velocity = Vector2.zero;
        if (distanceToPlayer < warningArea && direction.magnitude > 0.8)  //move closer but not in the player position
        {
            target = player.transform;
            playerInRange = true;
          
            transform.LookAt(player.transform.position);
            float move = speed * Time.deltaTime;
            moving = true;
            rb2D.velocity = direction * move;
        }
        else
        {
            target = null;
            moving = false;
            rb2D.velocity = Vector2.zero;
            return;
        }

    }



}
