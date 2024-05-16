using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbPhysics : MonoBehaviour
{
    [SerializeField] private InputActionProperty grabInputSource;
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private LayerMask grabLayer;
    [SerializeField] private PhysicsHand hand;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;

    private void Update()
    {
        if (isGrabbing)
        {
            hand.PreventCollision();
        }
        else
        {
            hand.ContinueCollision();
        }
    }

    private void FixedUpdate()
    {
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.1f;
        
        if (isGrabButtonPressed)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);

            if (nearbyColliders.Length > 0)
            {
                Rigidbody nearRb = nearbyColliders[0].attachedRigidbody;
                if(fixedJoint == null)
                    fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                if (nearRb)
                {
                    fixedJoint.connectedBody = nearRb;
                    fixedJoint.connectedAnchor = nearRb.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    fixedJoint.connectedAnchor = transform.position;
                }

                isGrabbing = true;
            }
        }
        else if (!isGrabButtonPressed && isGrabbing)
        {
            isGrabbing = false;
            if (fixedJoint)
            {
                Destroy(fixedJoint);
            }
        }
    }
}
