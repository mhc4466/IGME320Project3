using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatforms : MonoBehaviour
{
    public GameObject platform;
    public Transform spawnPoint;
    public Vector3 distMinBounds;
    public Vector3 distMaxBounds;
    public float maxY;      //limit how far up or down platforms can spawn
    public float maxZ;      //limit how far left or right platforms can spawn

    private Vector3 platformSize;

    // Start is called before the first frame update
    void Start()
    {
        platformSize = platform.GetComponent<MeshCollider>().bounds.size;
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
