using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WheelController2 : MonoBehaviour
{
    public float torque;
    public float maxWheelSpeed;

    private int turningRight = 0;
    private int turningLeft = 0;
    private int moving = 0;
    private int back = 0;
    private int braking = 0;

    private Transform robotPosition;
    private Quaternion robotRotation;

    public Rigidbody FL;
    public Rigidbody FR;
    public Rigidbody ML;
    public Rigidbody MR;
    public Rigidbody BL;
    public Rigidbody BR;

    void Start()
    {
        FL.maxAngularVelocity = maxWheelSpeed;
        FR.maxAngularVelocity = maxWheelSpeed;
        BL.maxAngularVelocity = maxWheelSpeed;
        BR.maxAngularVelocity = maxWheelSpeed;
        ML.maxAngularVelocity = maxWheelSpeed;
        MR.maxAngularVelocity = maxWheelSpeed;
    }
    // Update is called once per frame

    void Update()
    {

    }

    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");  //-1..0..1
        float h = Input.GetAxis("Horizontal");

        FL.AddTorque(FL.transform.up * (-v-h) * torque);
        FR.AddTorque(FR.transform.up * (-v+h) * torque);
        ML.AddTorque(ML.transform.up * (-v-h) * torque);
        MR.AddTorque(MR.transform.up * (-v+h) * torque);
        BL.AddTorque(BL.transform.up * (-v-h) * torque);
        BR.AddTorque(BR.transform.up * (-v+h) * torque);

        if (turningRight > 0)
        {
            FL.AddTorque(FL.transform.up * -1 * torque);
            FR.AddTorque(FR.transform.up * 1 * torque);
            ML.AddTorque(ML.transform.up * -1 * torque);
            MR.AddTorque(MR.transform.up * 1 * torque);
            BL.AddTorque(BL.transform.up * -1 * torque);
            BR.AddTorque(BR.transform.up * 1 * torque);
            turningRight--;
        }

        if (turningLeft > 0)
        {
            FL.AddTorque(FL.transform.up * 1 * torque);
            FR.AddTorque(FR.transform.up * -1 * torque);
            ML.AddTorque(ML.transform.up * 1 * torque);
            MR.AddTorque(MR.transform.up * -1 * torque);
            BL.AddTorque(BL.transform.up * 1 * torque);
            BR.AddTorque(BR.transform.up * -1 * torque);
            turningLeft--;
        }

        if (moving > 0)
        {
            FL.AddTorque(FL.transform.up * -1 * torque);
            FR.AddTorque(FR.transform.up * -1 * torque);
            ML.AddTorque(ML.transform.up * -1 * torque);
            MR.AddTorque(MR.transform.up * -1 * torque);
            BL.AddTorque(BL.transform.up * -1 * torque);
            BR.AddTorque(BR.transform.up * -1 * torque);
            moving--;
        }

        if (back > 0)
        {
            FL.AddTorque(FL.transform.up * 1 * torque);
            FR.AddTorque(FR.transform.up * 1 * torque);
            ML.AddTorque(ML.transform.up * 1 * torque);
            MR.AddTorque(MR.transform.up * 1 * torque);
            BL.AddTorque(BL.transform.up * 1 * torque);
            BR.AddTorque(BR.transform.up * 1 * torque);
            back--;
        }

        if (braking > 0)
        {
            FL.angularVelocity = Vector3.zero;
            FR.angularVelocity = Vector3.zero;
            ML.angularVelocity = Vector3.zero;
            MR.angularVelocity = Vector3.zero;
            BL.angularVelocity = Vector3.zero;
            BR.angularVelocity = Vector3.zero;
            braking--;
        }
    }

    public void TurnRight(int angle)
    {
        turningRight = (int)(0.3691 * (float)angle);
    }

    public void TurnLeft(int angle)
    {
        turningLeft = (int)(0.3691 * (float)angle);
    }

    public void MoveForward(int distance)
    {
        moving = (int)(7.4738*(float)distance);
    }

    public void MoveBack(int distance)
    {
        back = (int)(7.4738*(float)distance);
    }

    public void Brake()
    {
        braking = 1;
    }
}