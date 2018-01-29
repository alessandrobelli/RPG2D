using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    /// <summary>
    ///  The player movement <see cref="speed"/>
    /// </summary>
    public float speed = 5f;

    public GameObject blood;

    /// <summary>
    /// The Character <see cref="direction"/>
    /// </summary>
    protected Vector2 direction;

    protected Animator animator;
    protected Character currentTarget;
    protected float animationTime;

    protected float inverseMoveTime;
    protected SpriteRenderer spriteRenderer;

    public float moveTime = 1f;
    public float startingHealth;
    public float damage = 5f;
    public float combatRight = 0;
    public float fadeTime = 0.5f;
    public string combatAnimation = "combatUp";


    protected Rigidbody2D rb2D;

    public Stats health;

    public enum CURRENTLY_FACING
    {
        LEFT,
        UP,
        RIGHT,
        DOWN
    }

    protected CURRENTLY_FACING facing;


    /// <summary>
    /// Indicates if character is moving or not
    /// </summary>
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }


    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        animationTime = Time.time;

    }

    // Update is called once per frame
    protected virtual void Update ()
    {

        if(!IsMoving) rb2D.velocity = Vector2.zero;


    }


    private void LateUpdate()
    {
        /// keep z on 0
        if (transform.rotation.x != 0 || transform.rotation.y != 0 || transform.rotation.z != 0)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void FixedUpdate()
    {

        if (transform.tag == "Player" && !GameManager.isInDialog)
        {

            Move();
            Detect();
        }

        if (GameManager.isInDialog)
        {
            animator.SetLayerWeight(1, 1);
            animator.SetLayerWeight(2, 0);
        }


    }

    public void Detect()
    {
        // 8 = player
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Vector2[] directions =
          {
        new Vector2(transform.position.x,transform.position.y) + Vector2.down,
        new Vector2(transform.position.x,transform.position.y) + Vector2.left,
        new Vector2(transform.position.x,transform.position.y) + Vector2.up,
        new Vector2(transform.position.x,transform.position.y) + Vector2.right
    };

        Vector2 spriteDirection;
       
        switch (facing)
        {
            case (CURRENTLY_FACING.DOWN):
                spriteDirection = directions[0];
                combatAnimation = "combatDown";
                break;

            case (CURRENTLY_FACING.RIGHT):
                spriteDirection = directions[3];
                combatAnimation = "combatRight";
                break;


            case (CURRENTLY_FACING.LEFT):
                spriteDirection = directions[1];
                combatAnimation = "combatLeft";
                break;

            case (CURRENTLY_FACING.UP):
                spriteDirection = directions[2];
                combatAnimation = "combatUp";
                break;

            default:
                Debug.Log("shouldn't be here "+ facing);
                spriteDirection = directions[0];
                break;

        }

        


    }




    void Move()
    {

        //Makes sure that the player moves
        rb2D.velocity = direction.normalized * speed;
        //rb2D.MovePosition(new Vector2(transform.position.x + direction.x * speed * Time.deltaTime,transform.position.y + direction.y * speed * Time.deltaTime));


        if(direction.x != 0 || direction.y != 0)
        {

            // Animate player movement
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

        
    }

    public void AnimateMovement(Vector2 direction)
    {

        animator.SetLayerWeight(2, 0);
        animator.SetBool(combatAnimation, false);

        animator.SetLayerWeight(1, 1);
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);

    }


    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

   // set gameobject state to attack
    public void attack(Character obj,float damage)
    {
        currentTarget = obj;
        
        Stats objHealth = currentTarget.GetComponentInChildren<Stats>();

        objHealth.myCurrentValue -= damage;

        // show blood particle system
        Instantiate(blood, currentTarget.transform.position,Quaternion.identity);

        if (objHealth.myCurrentValue == 0)
        {
            // optionally trigger animation
            // animator in die state
            Debug.Log("destroy " + obj.gameObject);
            DestroyObject(obj.gameObject);

        }

    }


}
