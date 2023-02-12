using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WheelController : MonoBehaviour
{
    public float motor;
    public float steer;
    public float brake;

    public Rigidbody rig;

    public WheelCollider wcFL;
    public WheelCollider wcFR;
    public WheelCollider wcBL;
    public WheelCollider wcBR;

    public Transform FL;
    public Transform FR;
    public Transform BL;
    public Transform BR;

    // Update is called once per frame
    void FixedUpdate()
    {
        //steer and accelerate car (wasd, arrows, leftanalog gamepad)
        float vert = Input.GetAxis("Vertical");  //-1..0..1
        float horz = Input.GetAxis("Horizontal");
        wcFL.steerAngle = horz * steer;
        wcFR.steerAngle = horz * steer;
        wcBL.motorTorque = vert * motor;
        wcBR.motorTorque = vert * motor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
