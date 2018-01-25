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


        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                raycastHit = hit.Value;
                if (layerHit != layer)
                {
                    layerHit = layer;
                    layerChange(layerHit);

                }

                return;
            }
        }

        // Otherwise return background hit
        raycastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;


        if (currentLayerHit != Layer.RaycastEndStop) // layer did not change and is is not equal to RayCastEndStop
        {
            layerHit = Layer.RaycastEndStop;
        }

    }

    RaycastHit2D? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Input.mousePosition, distanceToBackground, layerMask);
      
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.GetComponentInParent<Transform>().name);
            return hit;
        }
        return null;
    }
}