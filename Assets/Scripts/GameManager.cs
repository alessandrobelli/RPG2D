using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool isInDialog = false;

    public bool IsInDialog
    {
        get
        {
            return isInDialog;
        }

        set
        {
            isInDialog = value;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

   void setInDialog()
    {
        isInDialog = true;
    }

    void notInDialog()
    {
        isInDialog = false;
    }


}
