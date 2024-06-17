using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegments : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody partentRb = transform.parent.GetComponent<Rigidbody>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        //rigidbody.mass = partentRb.mass + 5;
        HingeJoint joint = GetComponent<HingeJoint>();
        joint.connectedBody = partentRb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
