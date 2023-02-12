using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RobotActions : MonoBehaviour
{
    public string startingPosition;
    
    private bool hasFragment;
    private Rigidbody robot;
    private GameObject capturedSuppressor;
    private Transform capturedFragment;
    private Vector3 robotPosition;
    private Quaternion robotRotation;
    private float launchPower = 39;
    private float launchHeight = 10f;

    private Text inputCode;

    private WheelController2 wc2;
    private SuppressorActions sup;
    private FieldInfo field;

    // Start is called before the first frame update
    void Start()
    {
        field = GameObject.Find("Field").GetComponent<FieldInfo>();
        inputCode = GameObject.Find("Input Code").GetComponent<Text>();
        robot = this.GetComponent<Rigidbody>();
        wc2 = this.GetComponent<WheelController2>();
        sup = GameObject.Find("Field").GetComponent<SuppressorActions>();
        hasFragment = false;
    }

    // Update is called once per frame
    void Update()
    {
        robotPosition = robot.transform.position;
        robotRotation = robot.transform.rotation;

        if (Input.GetButtonDown("Fire1") && capturedSuppressor != null)
        {
            FireSuppressor();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            MoveFragment();
        }
        if (Input.GetKeyDown("z"))
        {
            OpenLowGoal();
        }
        if (Input.GetKeyDown("x"))
        {
            FireSuppressorLow();
        }
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Suppressor"))
        {   
            col.gameObject.transform.position = robotPosition + new Vector3(0.2f, 0.1f, 0f);
            capturedSuppressor = col.gameObject;
        }
    }
    
    public void FireSuppressor()
    {
        if(capturedSuppressor != null)
        {
            capturedSuppressor.GetComponent<Rigidbody>().AddForce(((Vector3.zero - robotPosition * launchPower) + new Vector3(0f, launchHeight * launchPower, 0f)) - robot.velocity);
        }
        capturedSuppressor = null;
    }

    public void FireSuppressorLow()
    {
        capturedSuppressor.GetComponent<Rigidbody>().AddForce((Vector3.zero - robotPosition * launchPower *7) + new Vector3(0f, 2f, 0f));
        capturedSuppressor = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Red") && (Time.time <= 15 ||  field.driverPhase == "endgame2"))
        {
            inputCode.text += "1";
        }

        if (other.gameObject.CompareTag("Blue") && (Time.time <= 15 ||  field.driverPhase == "endgame2"))
        {
            inputCode.text += "2";
        }

        if (other.gameObject.CompareTag("Yellow") && (Time.time <= 15 ||  field.driverPhase == "endgame2"))
        {
            inputCode.text += "3";
        }
    }

    public void MoveFragment()
    {
        if (hasFragment)
        {
            capturedFragment.position = GetClosestDrop(field.dropList).position;
            capturedFragment = null;
            hasFragment = false;
        }
        else
        {
            capturedFragment = GetClosestFragment(field.fragmentList);
            capturedFragment.position = robotPosition + new Vector3(0f, 0.1f, 0f);
            hasFragment = true;
        }
    }
    private Transform GetClosestFragment(Transform[] fragments)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in fragments)
        {
            float dist = Vector3.Distance(t.position, robotPosition);
            if (dist < minDist && dist < 2)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private Transform GetClosestDrop(Transform[] drops)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in drops)
        {
            float dist = Vector3.Distance(t.position, robotPosition);
            if (dist < minDist && dist < 2)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private Transform GetClosestDoor(Transform[] doors)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in doors)
        {
            float dist = Vector3.Distance(t.position, robotPosition);
            if (dist < minDist && dist < 2)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void OpenLowGoal()
    {
        if (Vector3.Distance(robotPosition, field.reactorWall.position) < 4)
        {
            field.reactorWall.gameObject.SetActive(false);
        }
    }

    public void CloseDoor()
    {
        GetClosestDoor(field.doorList).gameObject.SetActive(true);
    }
}