using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The Rate of Acceleration (0 none -> 1 instant)")] 
    [Range(0.0f, 1.0f)] public float acceleration;

    [Tooltip("The Rate of Deceleration (0 none -> 1 instant)")] 
    [Range(0.0f, 1.0f)] public float deceleration;

    [Tooltip("The Max Velocity")] 
    [Range(0.0f, 20.0f)] public float maxSpeed;

    public Rigidbody2D playerBody;
    public Rigidbody2D playerArm;

    [Range(-1.0f, 1.0f)] public float xInput;
    [Range(-1.0f, 1.0f)] public float yInput;

    void Start()
    {
        xInput = 0;
        yInput = 0;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        UpdateInput();
        MovePlayer();
        AimArm();
    }

    private void AimArm()
    {
        //Get pos of the mouse on screen
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Get angle between the current roation and the mouse pos
        float angle = Vector2.SignedAngle(transform.right, mousePos - (Vector2)transform.position);

        //Apply Rotation
        playerArm.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        
    }

    private void MovePlayer()
    {
        playerBody.velocity = new Vector2(xInput * maxSpeed, yInput * maxSpeed);

        playerBody.velocity = Mathf.Clamp(playerBody.velocity.magnitude, -maxSpeed, maxSpeed) * playerBody.velocity.normalized;
    }

    private void UpdateInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            xInput -= acceleration;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xInput += acceleration;
        }
        if (Input.GetKey(KeyCode.W))
        {
            yInput += acceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yInput -= acceleration;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && xInput != 0)
        {
            xInput *= 1 - deceleration; // 1 - (Mathf.Abs(xInput) / 1.1f)
            if (Mathf.Abs(xInput) < 0.1) xInput = 0;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && yInput != 0)
        {
            yInput *= 1 - deceleration;
            if (Mathf.Abs(yInput) < 0.1) yInput = 0;
        }

        xInput = Mathf.Clamp(xInput, -1.0f, 1.0f);
        yInput = Mathf.Clamp(yInput, -1.0f, 1.0f);
    }

    private void OnDrawGizmos()
    {
        Vector2 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, camPos);
    }
}
