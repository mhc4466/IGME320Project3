using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatforms : MonoBehaviour
{
    public GameObject platform;
    [Tooltip("SpawnPoint object transform, determines how far away new platforms spawn")]
    public Transform spawnPoint;
    [Tooltip("Minimum x, y, and z offset between platforms")]
    public Vector3 distMinBounds;
    [Tooltip("Maximum x, y, and z offset between platforms")]
    public Vector3 distMaxBounds;
    [Tooltip("Limit how far up or down platforms can spawn")]
    public float maxY;      //limit how far up or down platforms can spawn
    [Tooltip("Limit how far left or right platorms can spawn")]
    public float maxZ;      //limit how far left or right platforms can spawn
    [Tooltip("Applies a multiplier to distance between platforms")]
    public float separateMultiplier;   //platforms must be separated by this multiplier
    private Bounds platformSize;

    // Start is called before the first frame update
    void Start()
    {
        platformSize = platform.GetComponent<MeshCollider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= spawnPoint.position.x)
        {
            //random x, y, and z distance within range
            float distX = Random.Range(distMinBounds.x, distMaxBounds.x);
            float distY = Random.Range(distMinBounds.y, distMaxBounds.y);
            float distZ = Random.Range(distMinBounds.z, distMaxBounds.z);

            //limit where platforms can spawn on y and z
            if (Mathf.Abs(transform.position.y + distY) >= maxY)
                distY = 0.0f;
            if (Mathf.Abs(transform.position.z + distZ) >= maxZ)
                distZ = 0.0f;

            //move platforms apart if they overlap
            if (Mathf.Abs(distX) <= platformSize.size.x * separateMultiplier)
            {
                int randomDirection = Random.Range(1, 5);

                //move in a random direction 
                switch (randomDirection)
                {
                    //up
                    case 1:
                        distY += platformSize.size.y * separateMultiplier;
                        break;
                    //down
                    case 2:
                        distY -= platformSize.size.y * separateMultiplier;
                        break;
                    //left
                    case 3:
                        distZ += platformSize.size.z * separateMultiplier;
                        break;
                    //right
                    case 4:
                        distZ -= platformSize.size.z * separateMultiplier;
                        break;
                }
            }
            

            transform.position = new Vector3(transform.position.x + distX,
                                                transform.position.y + distY,
                                                transform.position.z + distZ);
            
            //Object pooler code that "spawns" a platform
            GameObject newPlatform = ObjectPooler.SharedInstance.GetPooledObject("Platform");
            if (newPlatform != null)
            {
                newPlatform.transform.position = transform.position;
                //newPlatform.transform.rotation = [SPAWN ROTATION];
                newPlatform.SetActive(true);
            }
        }
    }
}
