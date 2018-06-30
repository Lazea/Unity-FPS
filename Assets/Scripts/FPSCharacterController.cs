using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterController : MonoBehaviour {

    // Character movement speed parameters
    public float walkSpeed = 3f;
    public float runSpeed = 7f;
    public float airSpeed = 1f;
    public float crouchSpeed = 2f;
    float speed;

    // Character velocities
    Vector3 velocity;
    Vector3 launchVelocity;

    // Character fall parameters
    float g = 10f;
    public float jumpPower = 6f;
    float fallSpeed;
    float groundedFallSpeed;
    public float maxFallSpeed = 25f;

    // Character ground check parameters
    Ray groundSphereRay;
    float groundSphereRadius = 0.5f;
    float groundSphereRayDistance = 0.57f;
    bool isGrounded;
    bool wasGrounded;
    bool jumped = false;

    // Character slope parameters
    float groundSlope;
    Vector3 slopeDir;
    Vector3 slideVelocity;

    // Input parameters
    float h;
    float v;
    Vector3 inputDir;
    bool run;
    bool crouch;
    bool jump;

    CharacterController controller;

    // Use this for initialization
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        run = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);

        if (isGrounded)
        {
            jump = Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            jump = false;
        }

        // Compute move direction
        Vector3 targetDir = new Vector3(h, 0f, v).normalized;
        targetDir = transform.forward * targetDir.z + transform.right * targetDir.x;
        if (targetDir.magnitude > 0)
        {
            inputDir = Vector3.Lerp(inputDir, targetDir, 0.1f);
        }
        else
        {
            inputDir = Vector3.Lerp(inputDir, targetDir, 0.8f);
        }

        // Ground Check
        groundSphereRay = new Ray(transform.position, -transform.up);
        RaycastHit hit = new RaycastHit();
        if (Physics.SphereCast(groundSphereRay, groundSphereRadius, out hit, groundSphereRayDistance))
        {
            int groundLayer = LayerMask.NameToLayer("Ground");
            int platformLayer = LayerMask.NameToLayer("DynamicPlatform");
            int hitLayer = hit.transform.gameObject.layer;
            if (hitLayer == groundLayer || hitLayer == platformLayer)
            {
                isGrounded = true;

                Vector3 groundContactNorm = hit.normal;
                slopeDir = groundContactNorm;
                slopeDir.y = 0f;
                slopeDir.Normalize();

                groundSlope = Vector3.Angle(transform.up, groundContactNorm);
            }

            if (hitLayer == platformLayer)
            {
                transform.parent = hit.transform;
            }
            else
            {
                transform.parent = null;
            }
        }
        else
        {
            isGrounded = false;
            transform.parent = null;
        }

        // Movement logic
        if (isGrounded && !jumped)
        {
            speed = walkSpeed;
            fallSpeed = 0f;
            groundedFallSpeed = 20f;

            launchVelocity = velocity;
            launchVelocity.y = 0f;

            slideVelocity = Vector3.zero;
            if (groundSlope > 55f)
            {
                slideVelocity = slopeDir * 1f;
            }

            if (jump)
            {
                fallSpeed = -jumpPower;

                jumped = true;
            }
            else if (run)
            {
                if (Vector3.Angle(transform.forward, inputDir) < 80f)
                {
                    speed = runSpeed;
                }
            }
            else if (crouch)
            {
                speed = crouchSpeed;
            }

            velocity = slideVelocity;
            velocity += -Vector3.up * groundedFallSpeed;
        }
        else
        {
            jumped = false;

            speed = airSpeed;
            slideVelocity = Vector3.zero;

            fallSpeed += g * Time.deltaTime;
            if (fallSpeed >= maxFallSpeed)
            {
                fallSpeed = maxFallSpeed;
            }
            groundedFallSpeed = 0f;

            velocity = launchVelocity;
            velocity += -Vector3.up * fallSpeed;
        }

        // Apply velocity
        velocity += inputDir * speed;

        controller.Move(velocity * Time.deltaTime);
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        // Draw ground sphere cast
        Gizmos.DrawWireSphere(groundSphereRay.origin + groundSphereRay.direction * groundSphereRayDistance, groundSphereRadius);
    }
}
