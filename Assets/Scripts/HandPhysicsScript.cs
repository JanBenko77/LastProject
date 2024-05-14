using System.Linq;
using UnityEngine;

public class HandPhysicsScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Renderer nonPhysicsHand;
    [SerializeField] private float nonPhysicsHandDistance = 0.05f;
    private float maxDistance = 0.5f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        float distance = Vector3.Distance(transform.position,target.position);
        if(distance > nonPhysicsHandDistance){
            nonPhysicsHand.enabled = true;
            if(distance > maxDistance)
                transform.position = target.position;

        }
        else
            nonPhysicsHand.enabled = false;
    }
    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        Quaternion rotationDiffrence = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiffrence.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDiffrenceInDegrees = angleInDegree* rotationAxis;

        rb.angularVelocity = rotationDiffrenceInDegrees * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }
}