using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : Character {

    public Flowchart dialog;

    public override void Start()
    {
        
    }

    protected override void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (Input.GetKey(KeyCode.Q))
        {
            dialog.SendFungusMessage("click"); 
        }

    }

}
