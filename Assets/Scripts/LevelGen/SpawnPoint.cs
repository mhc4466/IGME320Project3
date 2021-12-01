using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float renderDist;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x + renderDist, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //move spawn if player is halfway to spawn point
        if(transform.position.x - renderDist <= player.position.x)
            transform.position = new Vector3(transform.position.x + renderDist, transform.position.y, transform.position.z);
    }
}
