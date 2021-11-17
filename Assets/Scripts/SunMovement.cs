using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public float sunSpeed = 12f;
    public Transform player;
    float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = GetComponent<SphereCollider>().radius; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (sunSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private bool CheckForPlayerCollision()
    {
        float distanceToPlayerX = player.position.x - transform.position.x;
        float distanceToPlayerY = player.position.y - transform.position.y;
        float distanceToPlayerZ = player.position.z - transform.position.z;

        float squareSum = (distanceToPlayerX * distanceToPlayerX) + (distanceToPlayerY * distanceToPlayerY) + (distanceToPlayerZ * distanceToPlayerZ);
        if(squareSum < (radius * radius))
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Transition to GameOver Scene
            Debug.Log("Player killed");
        }
    }
}
