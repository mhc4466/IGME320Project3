using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingGun : MonoBehaviour
{
    //line renderer and basic varaibles
    private LineRenderer lr;
    private Vector3 grapplePoint;
    [SerializeField] private LayerMask whatIsGrappleable;
    [SerializeField] private Image cursorComponent;
    //transforms
    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform player;
    //shooting distance
    [SerializeField] private float maxShootDistance =100f;
    //grappling Hook Values
    [SerializeField] private float jointMaxDistance = 0.08f;
    [SerializeField] private float jointMinDistance = 0.25f;
    [SerializeField] private float jointSpring =4.5f;
    [SerializeField] private float jointDamper =7.0f;
    [SerializeField] private float springMassScale =4.5f;
    private SpringJoint joint;
    private GameObject grappledObject;
    private Vector3 displacement;
    private bool connected;
    private AudioSource grappleSound;
    private void Awake()
    {
        //get the line renderer
        lr = GetComponent<LineRenderer>();
        grappleSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            ColorCursor();
            //get player input
            if (Input.GetMouseButtonDown(0))
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
                grappleSound.Stop();
            }
            else if (Input.GetMouseButton(0) && connected)
            {
                joint.connectedAnchor = grappledObject.transform.position + displacement;
            }
        }
    }
    private void LateUpdate()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            DrawRope();
        }
    }
    private void StartGrapple() 
    {
        //raycast to see if you find a point you can grapple to
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxShootDistance,whatIsGrappleable.value))
        {
            grappleSound.Play();
            connected = true;
            //set values for grapple and joint
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            grappledObject = hit.collider.gameObject;
            displacement = grapplePoint - grappledObject.transform.position;

            //set distance for grapple point
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //values determining how the physics behave
            joint.maxDistance = distanceFromPoint * jointMaxDistance;
            joint.minDistance = distanceFromPoint * jointMinDistance;

            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = springMassScale;

            lr.positionCount = 2;
        }
        else
            connected = false;
    }
    private void StopGrapple() 
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void DrawRope() 
    {
        if (!joint) return; 
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grappledObject.transform.position + displacement);
        

    }

    /// <summary>
    /// Sets the color of the cursor to green if it is over a grappleable object, and white if it is not
    /// </summary>
    private void ColorCursor()
    {
        RaycastHit hit; //Variable to store the raycast
        //If the raycast hits a ground object in range, set the cursor to green
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxShootDistance, whatIsGrappleable))
            cursorComponent.color = Color.green;
        //If the raycast doesn't reach anything within the distance, set the cursor to white
        else
            cursorComponent.color = Color.white;
    }
}
