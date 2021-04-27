using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    public float maxTailAngle = 45.0f;
    public float currentAngle = 0.0f;
    // Start is called before the first frame update
    public float baseAngleIncrementFactor = 100.0f;
    private float tailLength;
    private float angleFactorSign = 1.0f;
    private float currentSpeed = 0.0f;
    private Vector3 lastPosition;
    void Start()
    {
        tailLength = transform.GetChild(0).GetComponent<Collider>().bounds.size.y;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = Vector3.Magnitude(transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        float angleStep = angleFactorSign * baseAngleIncrementFactor * currentSpeed * Time.deltaTime;
        currentAngle += angleStep;
        if (currentAngle >= maxTailAngle)
        {
            angleFactorSign = -1;

        }
        if (currentAngle <= -maxTailAngle)
        {
            angleFactorSign = 1;
        }

        var parentTransfrom = transform.parent.transform;
        transform.RotateAround(transform.position - parentTransfrom.forward * tailLength / 2.0f, Vector3.up, angleStep);
    }
}
