using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3 (transform.position.x + 0.02f, transform.position.y, transform.position.z + 0.02f);
    }
}
