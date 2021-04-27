using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallController : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;

    public float arriveUnits = 10.0f;
    public float maxWalkingSpeed = 0.5f;
    public float rotatingSpeed = 5.0f;
    public float walkingSpeed = 0.0f;
    private float waklkingSpeadIncreaseFactor = 0.5f;
    // Start is called before the first frame update
    private float ballSpeed = 0.0f;
    private Vector3 ballLastPosition;

    private Vector3 currentWaypoint;
    private Vector3 target;

    private Rigidbody ballRigidbody;

    private bool isBallPickedUp = false;
    public void Init()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBallPickedUp)
        {
            currentWaypoint = ball.transform.position - transform.forward * 2.0f;
            target = ball.transform.position;
        }
        else
        {
            target = player.transform.position;
            ball.transform.position = transform.position + transform.forward * 1.5f;

            ball.transform.position = new Vector3(ball.transform.position.x, 1.0f, ball.transform.position.z); // Carry ball above the dog
            ball.transform.rotation = Quaternion.Euler(0, 0, 0);
            currentWaypoint = player.transform.position + player.transform.forward * arriveUnits; // Bring it infront of player
            // Dog is very close to the target position
            if (Vector3.Distance(transform.position, currentWaypoint) < 3.0f)
            {
                // Drop the ball
                // Change state
                ballSpeed = 0.0f;
                isBallPickedUp = false;
                ballSpeed = 0.0f;
                GetComponent<StateMachine>().ChangeState(new DogFollowPlayerState());
            }
        }

        walkingSpeed += waklkingSpeadIncreaseFactor * Time.deltaTime;
        walkingSpeed = Mathf.Clamp(walkingSpeed, 0.0f, maxWalkingSpeed);
        currentWaypoint.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, currentWaypoint, walkingSpeed * Time.deltaTime);

        var dogRotation = Quaternion.LookRotation(target - transform.position);
        dogRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, dogRotation.eulerAngles.y, dogRotation.eulerAngles.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, dogRotation, rotatingSpeed * Time.deltaTime);

        if (ballRigidbody.velocity.magnitude < 1.0f && Vector3.Distance(transform.position, currentWaypoint) < 1.5f && !isBallPickedUp)
        {
            // Pick up the ball
            isBallPickedUp = true;
            walkingSpeed = 0.0f;
        }
    }
}
