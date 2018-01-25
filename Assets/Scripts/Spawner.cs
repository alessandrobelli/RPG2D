using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public GameObject[] attackerPrefab;
    public float seenEveryXseconds = 4f;

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject thisAttacker in attackerPrefab)
        {
            if (isTimeToSpawn(thisAttacker))
            {
                Spawn(thisAttacker);
            }
        }

    }


    void Spawn(GameObject myGameObject)
    {
        GameObject myAttacker = Instantiate(myGameObject) as GameObject;
        myAttacker.transform.parent = transform;

        myAttacker.transform.position = transform.position;
    }

    bool isTimeToSpawn(GameObject attacker)
    {
        float spawnsPerSecond = 1;
        
        if (Time.deltaTime >= spawnsPerSecond)
        {
            Debug.LogWarning("Spawn capped by frame rate");
        }

        float treshold = spawnsPerSecond * Time.deltaTime / 4;
        
        return (Random.value < treshold);
    }
}

