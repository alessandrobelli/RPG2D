using System;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit2D raycastHit;


    RaycastHit2D playerHit;
    Ray ray;
    GameObject player;
    Material previousFrontObject;

    public RaycastHit2D hit
    {
        get { return raycastHit; }
    }

    Layer layerHit;
    public Layer currentLayerHit
    {
        get { return layerHit; }
    }

    public delegate void OnLayerChange(Layer newLayer); // declare new delegate type
    public event OnLayerChange layerChange; // instantiate observers

    private void Awake()
    {
        viewCamera = Camera.main;

    }
    void Start() // TODO Awake?
    {
        player = GameObject.FindGameObjectWithTag("Player");
   

    }

    void Update()
    {

        RaycastHit2D hitt = CheckForCollisions(Input.mousePosition);
     
    }
  

    public RaycastHit2D CheckForCollisions(Vector3 dir)
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("MOUSE POSITION: " + hit.transform.name);

            int layer = hit.transform.gameObject.layer;

            switch (layer)
            {
                case (9):
                    layerChange(Layer.Enemy);
                    break;
                default:

                    layerChange(Layer.Walkable);
                    break;

            }
        }
        else
        {
            layerChange(Layer.Walkable);
        }


        return hit;
    }

}