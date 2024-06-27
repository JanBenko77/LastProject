using System;
using UnityEngine;

public class PhysicsHand : MonoBehaviour
{
    [Header("PID")]
    [SerializeField] private float frequency = 50f;
    [SerializeField] private float damping = 1f;
    [SerializeField] private float rotFrequency = 100f;
    [SerializeField] private float rotDamping = 0.9f;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform target;
    Rigidbody rb;

    [Header("Hooke's Law")]
    [SerializeField] private float climbForce = 1000f;
    [SerializeField] private float climbDrag = 500f;
    [SerializeField] private Vector3 maxForce = new Vector3(1000f, 1000f, 1000f);
    private Vector3 previousPosition;
    private Vector3 accumilatedForce;
    private bool isColliding = false;

    private BoxCollider boxCollider;

    [Header("Grabbing")]
    public PhysicsHandInteractor interactor;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
   //public static bool isClimbing = false;
    [SerializeField] private RenderSettings renderSettings;
    public bool isGrabbing{get; private set;}

    public bool isHovering{get; private set;}
    private void Start()
    {
        if(meshRenderer == null)
            meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        interactor = target.GetComponent<PhysicsHandInteractor>();
        interactor.physicsHand = this;
        transform.position = target.position;
        transform.rotation = target.rotation;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = float.PositiveInfinity;
        previousPosition = transform.position;
        boxCollider = GetComponent<BoxCollider>();
        EventBus<OnClimbHoverEnter>.OnEvent += OnHoverEnter;
        EventBus<OnClimbHoverExit>.OnEvent += OnHoverExit;
        EventBus<OnGrabEnter>.OnEvent += OnGrabEnter;
        EventBus<OnGrabExit>.OnEvent += OnGrabExit;
    }

    private void LateUpdate()
    {
        PIDMovement();
        PIDRotation();
        if (isGrabbing) ClimbLaw();
        else if (isColliding) HookesLaw();
    }

    private void PIDMovement()
    {
        float kp = (6f * frequency) * (6f * frequency) * 0.25f;
        float kd = 4.5f * frequency * damping;
        float g = 1 / (1 + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Vector3 force = (target.position - transform.position) * ksg + (playerRb.velocity - rb.velocity) * kdg;
        Mathf.Clamp(force.x, -maxForce.x, maxForce.x);
        Mathf.Clamp(force.y, -maxForce.y, maxForce.y);
        Mathf.Clamp(force.z, -maxForce.z, maxForce.z);
        accumilatedForce = force;
        rb.AddForce(accumilatedForce, ForceMode.Acceleration);
    }

    private void PIDRotation()
    {
        float kp = (6f * rotFrequency) * (6f * rotFrequency) * 0.25f;
        float kd = 4.5f * rotFrequency * rotDamping;
        float g = 1 / (1 + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Quaternion q = target.rotation * Quaternion.Inverse(transform.rotation);
        if (q.w < 0)
        {
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        q.ToAngleAxis(out float angle, out Vector3 axis);
        axis.Normalize();
        axis *= Mathf.Deg2Rad;
        Vector3 torque = ksg * axis * angle + -rb.angularVelocity * kdg;
        rb.AddTorque(torque, ForceMode.Acceleration);
    }

    private void HookesLaw()
    {
        Vector3 displacementFromResting = transform.position - target.position;
        Vector3 force = displacementFromResting * climbForce;
        float drag = GetDrag();

        playerRb.AddForce(force, ForceMode.Acceleration);
        playerRb.AddForce(drag * -playerRb.velocity * climbDrag, ForceMode.Acceleration);
    }

    private void ClimbLaw()
    {
        playerRb.AddForce(-accumilatedForce, ForceMode.Acceleration);
    }
    private float GetDrag()
    {
        Vector3 handVelocity = (target.localPosition - previousPosition) / Time.fixedDeltaTime;
        float drag = 1 / handVelocity.magnitude + 0.01f;
        drag = drag >= 1 ? 1 : drag;
        drag = drag <= 0.03f ? 0.03f : drag;
        previousPosition = transform.position;
        return drag;
    }


    public void DestroyJoint(){
        isGrabbing = false;
        Destroy(GetComponent<FixedJoint>());
    }

    public void CreateJoint(){
        isGrabbing = true;
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    public void PreventCollision()
    {
        boxCollider.enabled = false;
    }

    public void ContinueCollision()
    {
        boxCollider.enabled = true;
    }

    public void SetMaterial(){
        if(isGrabbing)
            meshRenderer.material = renderSettings.grabMaterial;
        else if(isHovering)
            meshRenderer.material = renderSettings.hoverMaterial;
        else
            meshRenderer.material = renderSettings.defaultMaterial;
    }

    void OnHoverEnter(OnClimbHoverEnter args){
        if(args.hand == this) {
            isHovering = true;
            SetMaterial();
        }
    }

    void OnHoverExit(OnClimbHoverExit args){
        if(args.hand == this) {
            isHovering = false;
            SetMaterial();
        }
    }

    void OnGrabEnter(OnGrabEnter args){
        if(args.hand != this)
            DestroyJoint();
        else 
            CreateJoint();
        SetMaterial();
    }
    void OnGrabExit(OnGrabExit args){
        if(args.hand == this)
            DestroyJoint();
        SetMaterial();
    }

    void OnDestroy(){
        EventBus<OnClimbHoverEnter>.OnEvent -= OnHoverEnter;
        EventBus<OnClimbHoverExit>.OnEvent -= OnHoverExit;
        EventBus<OnGrabEnter>.OnEvent -= OnGrabEnter;
        EventBus<OnGrabExit>.OnEvent -= OnGrabExit;
    }
}
