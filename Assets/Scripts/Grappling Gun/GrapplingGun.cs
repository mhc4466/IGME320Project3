using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    //line renderer and basic varaibles
    private LineRenderer lr;
    private Vector3 grapplePoint;
    [SerializeField] private LayerMask whatIsGrappleable;
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
    private void Awake()
    {
        //get the line renderer
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //get player input
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else 
        {
            StopGrapple();
        }
    }
    private void LateUpdate()
    {
        DrawRope();
    }
    private void StartGrapple() 
    {
        //raycast to see if you find a point you can grapple to
        RaycastHit hit;
        if (Physics.Raycast(camera.position,camera.forward,out hit,maxShootDistance)) 
        {
            //set values for grapple and joint
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            //set distance for grapple point
            float distanceFromPoint = Vector3.Distance(player.position,grapplePoint);

            //values determining how the physics behave
            joint.maxDistance = distanceFromPoint * jointMaxDistance;
            joint.minDistance = distanceFromPoint * jointMinDistance;

            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = springMassScale;
        }
    }
    private void StopGrapple() 
    {
    
    }

    private void DrawRope() 
    {
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }
}