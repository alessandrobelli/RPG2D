using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour {

    private SpriteRenderer parentRenderer;
    private List<Obstacle> obstacles = new List<Obstacle>();

[SerializeField] int sortingLayer = 200;



	// Use this for initialization
	void Start () {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Manage the collision with the obstacles
    /// Go behind the obstacles
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle" )
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeOut();
            if(obstacles.Count == 0 || o.mySpriteRenderer.sortingOrder-1 < parentRenderer.sortingOrder)
            {
                parentRenderer.sortingOrder = o.mySpriteRenderer.sortingOrder - 1;
            }
            obstacles.Add(o);

        }
    }

    /// <summary>
    /// manage the collision exit on the obstacles
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Obstacle")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeIn();
            obstacles.Remove(o);

            if(obstacles.Count == 0)
            {
                parentRenderer.sortingOrder = sortingLayer;
            }
            else
            {
                obstacles.Sort();
                parentRenderer.sortingOrder = obstacles[0].mySpriteRenderer.sortingOrder - 1;
            }

        }

    }

}
