using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerController : MonoBehaviour
{
    public GameObject player;
    public float arriveUnits = 10.0f;
    public float maxWalkingSpeed = 0.5f;
    public float rotatingSpeed = 0.5f;
    public float walkingSpeed = 0.0f;
    private float waklkingSpeadIncreaseFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        walkingSpeed += waklkingSpeadIncreaseFactor * Time.deltaTime;
        walkingSpeed = Mathf.Clamp(walkingSpeed, 0.0f, maxWalkingSpeed);
        Vector3 arrivePositon = player.transform.position + player.transform.forward * arriveUnits;
        arrivePositon.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, arrivePositon, walkingSpeed * Time.deltaTime);

        var playerRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        playerRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, playerRotation.eulerAngles.y, playerRotation.eulerAngles.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotatingSpeed * Time.deltaTime);
    }
}
