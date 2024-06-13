using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement")]
    private Rigidbody rb;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 15f;
    [SerializeField] private float jumpForce = 5f;

    private bool isSprinting = false;

    private float maxVelocityChange = 10f;
    #endregion

    #region Player Look Variables
    private Camera cam;
    private bool camLocked = false;
    private float maxLookAngle = 70f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool lockCursor = true;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        CameraMovement();
        PlayerJump();
        PlayerSprint();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region Movement
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGrounded())
            {
                rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            }
        }
    }

    private void PlayerSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    private bool CheckGrounded()
    {
        Vector3 origin = transform.position;
        Vector3 direction = new Vector3(0f, -1f, 0f);
        float maxDistance = 1.3f;
        return Physics.Raycast(origin, direction, out _, maxDistance);
    }

    private void MovePlayer()
    {
        if (rb != null)
        {
            Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            //speed = isSprinting ? sprintSpeed : speed;
            moveVector = transform.TransformDirection(moveVector) * speed;

            Vector3 velocity = rb.velocity;
            Vector3 velocityOffset = moveVector - velocity;
            velocityOffset.x = Mathf.Clamp(velocityOffset.x, -maxVelocityChange, maxVelocityChange);
            velocityOffset.z = Mathf.Clamp(velocityOffset.z, -maxVelocityChange, maxVelocityChange);
            velocityOffset.y = 0;

            rb.AddForce(velocityOffset, ForceMode.VelocityChange);
        }
    }

    #endregion

    #region Player Look
    private void CameraMovement()
    {
        if (!camLocked)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X");

            pitch -= Input.GetAxis("Mouse Y");

            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
           if(cam != null) cam.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }

    #endregion

}
