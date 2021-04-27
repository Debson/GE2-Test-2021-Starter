using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallController : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public float maxWalkingSpeed = 0.5f;
    public float rotatingSpeed = 5.0f;
    public float walkingSpeed = 0.0f;
    private float waklkingSpeadIncreaseFactor = 0.5f;
    // Start is called before the first frame update
    private float ballSpeed = 0.0f;
    private Vector3 ballLastPosition;

    private Vector3 currentWaypoint;

    private bool isBallPickedUp = false;
    void Start()
    {
        GetComponent<AudioSource>().Play(0);
        ballLastPosition = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBallPickedUp) currentWaypoint = ball.transform.position;
        

        walkingSpeed += waklkingSpeadIncreaseFactor * Time.deltaTime;
        walkingSpeed = Mathf.Clamp(walkingSpeed, 0.0f, maxWalkingSpeed);
        Vector3 arrivePositon = ball.transform.position + ball.transform.forward;
        arrivePositon.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, arrivePositon, walkingSpeed * Time.deltaTime);

        var dogRotation = Quaternion.LookRotation(ball.transform.position - transform.position);
        dogRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, dogRotation.eulerAngles.y, dogRotation.eulerAngles.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, dogRotation, rotatingSpeed * Time.deltaTime);

        if (ballSpeed <= 0.1f)
        {
            // Pick up the ball
            isBallPickedUp = true;
        }
        else
        {
            ballSpeed = Vector3.Magnitude(ball.transform.position - ballLastPosition) / Time.deltaTime;
        }


    }
}
