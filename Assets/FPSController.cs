﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class FPSController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject ball;
    public GameObject dog;
    public float speed = 50.0f;
    public float lookSpeed = 150.0f;
    public float ballThrowForce = 100.0f;

    public bool allowPitch = true;

    private State dogChaseBallState = new DogChaseBallState();

    public GUIStyle style;
    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (mainCamera == null)
        {
            mainCamera = Camera.main.gameObject;
        }
        Invoke("Activate", 2);
    }

    void Yaw(float angle)
    {
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = rot * transform.rotation;
    }

    void Roll(float angle)
    {
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rot * transform.rotation;
    }

    float invcosTheta1;

    void Pitch(float angle)
    {
        float theshold = 0.95f;
        if ((angle > 0 && invcosTheta1 < -theshold) || (angle < 0 && invcosTheta1 > theshold))
        {
            return;
        }
        // A pitch is a rotation around the right vector
        Quaternion rot = Quaternion.AngleAxis(angle, transform.right);

        transform.rotation = rot * transform.rotation;
    }

    void Walk(float units)
    {
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        transform.position += forward * units;
    }

    void Fly(float units)
    {
        transform.position += Vector3.up * units;
    }

    void Strafe(float units)
    {
        transform.position += mainCamera.transform.right * units;

    }

    bool active = false;

    void Activate()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }
        //Cursor.lockState = CursorLockMode.Confined;

        float mouseX, mouseY;
        float speed = this.speed;

        invcosTheta1 = Vector3.Dot(transform.forward, Vector3.up);

        float runAxis = 0; // Input.GetAxis("Run Axis");

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.LeftShift) || runAxis != 0)
        {
            speed *= 5.0f;
        }

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        Yaw(mouseX * lookSpeed * Time.deltaTime);
        if (allowPitch)
        {
            Pitch(-mouseY * lookSpeed * Time.deltaTime);
        }

        float contWalk = Input.GetAxis("Vertical");
        float contStrafe = Input.GetAxis("Horizontal");
        Walk(contWalk * speed * Time.deltaTime);
        Strafe(contStrafe * speed * Time.deltaTime);

        var dogStateMachine = dog.GetComponent<StateMachine>();
        if (Input.GetKeyDown(KeyCode.Space) && dogStateMachine.currentState != dogChaseBallState)
        {
            var ballObject = Instantiate(ball, transform.position, transform.rotation);
            var rigidBody = ballObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.forward * ballThrowForce);

            dog.GetComponent<FollowBallController>().ball = ballObject;
            dog.GetComponent<AudioSource>().Play(0);
            dogStateMachine.ChangeState(dogChaseBallState);
        }
    }
}