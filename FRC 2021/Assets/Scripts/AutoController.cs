using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AutoController : MonoBehaviour
{
    private WheelController2 wc2;
    private FieldInfo field;
    private RobotActions robot;
    // Start is called before the first frame update
    void Start()
    {
        wc2 = this.GetComponent<WheelController2>();
        field = GameObject.Find("Field").GetComponent<FieldInfo>();
        robot = this.GetComponent<RobotActions>();

        if (field.driverPhase == "teleop")
        {
            StartCoroutine(Auto());
        }
        else if (field.driverPhase == "endgame1")
        {
            StartCoroutine(Endgame1());
        }
        else
        {
            StartCoroutine(Endgame2());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Auto()
    {
        if(robot.startingPosition == "right")
        {
            yield return new WaitForSeconds(2f);
            wc2.MoveForward(4);
            yield return new WaitForSeconds(2);
            wc2.TurnRight(40);
            yield return new WaitForSeconds(1);
            wc2.MoveForward(8);
            yield return new WaitForSeconds(15);
        }

        if(robot.startingPosition == "middle")
        {
            yield return new WaitForSeconds(2f);
            wc2.MoveForward(13);
            yield return new WaitForSeconds(18);
        }

        if(robot.startingPosition == "left")
        {
            yield return new WaitForSeconds(2f);
            wc2.MoveForward(4);
            yield return new WaitForSeconds(2);
            wc2.TurnLeft(35);
            yield return new WaitForSeconds(1);
            wc2.MoveForward(8);
            yield return new WaitForSeconds(3);
            wc2.TurnLeft(7);
            yield return new WaitForSeconds(0.5f);
            wc2.MoveBack(11);
            yield return new WaitForSeconds(4f);
            
            robot.FireSuppressor();
            yield return new WaitForSeconds(0.5f);
            wc2.TurnRight(27);
            yield return new WaitForSeconds(1);
            wc2.MoveBack(2);
            yield return new WaitForSeconds(6);
        }
        StartCoroutine(Teleop1());
    }
    private IEnumerator Teleop1()
    {
        if (robot.startingPosition == "right")
        {
            wc2.TurnLeft(15);
            yield return new WaitForSeconds(0.5f);
            wc2.MoveBack(11);
            yield return new WaitForSeconds(4.5f);
            robot.MoveFragment();

            wc2.MoveForward(9);
            yield return new WaitForSeconds(2f);
            robot.MoveFragment();

            yield return new WaitForSeconds(5f);
            robot.MoveFragment();
            wc2.MoveBack(8);
            yield return new WaitForSeconds(5f);
        }
        if (robot.startingPosition == "middle")
        {
            wc2.MoveBack(3);
            yield return new WaitForSeconds(1f);
            wc2.TurnLeft(30);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(1);
            yield return new WaitForSeconds(1f);
            wc2.TurnRight(30);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(3);
            yield return new WaitForSeconds(1);
            robot.MoveFragment();

            wc2.MoveForward(2);
            yield return new WaitForSeconds(2.5f);
            robot.MoveFragment();

            wc2.TurnLeft(60);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(2);
            yield return new WaitForSeconds(2.5f);
            
            robot.FireSuppressor();
            yield return new WaitForSeconds(2f);
            robot.FireSuppressor();
            yield return new WaitForSeconds(4f);
        }
        if (robot.startingPosition == "left")
        {
            robot.MoveFragment();

            wc2.MoveForward(4);
            yield return new WaitForSeconds(2);
            wc2.TurnLeft(40);
            yield return new WaitForSeconds(1);
            wc2.MoveForward(8);
            yield return new WaitForSeconds(5f);
            robot.MoveFragment();

            yield return new WaitForSeconds(2f);
            robot.FireSuppressor();
            wc2.TurnRight(6);
            yield return new WaitForSeconds(0.5f);
            wc2.MoveBack(9);
            yield return new WaitForSeconds(3.5f);
            robot.FireSuppressor();
            yield return new WaitForSeconds(3f);
        }
        StartCoroutine(Teleop2());
    }
    private IEnumerator Teleop2()
    {
        if (robot.startingPosition == "right")
        {
            robot.MoveFragment();
            yield return new WaitForSeconds(1f);

            robot.MoveFragment();
            wc2.MoveForward(10);
            yield return new WaitForSeconds(17.5f);
            robot.MoveFragment();
        }
        if (robot.startingPosition == "middle")
        {
            wc2.TurnRight(10);
            yield return new WaitForSeconds(1f);
            wc2.MoveForward(4);
            yield return new WaitForSeconds(3f);

            robot.MoveFragment();
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(8);
            yield return new WaitForSeconds(3f);
            robot.MoveFragment();
            yield return new WaitForSeconds(1f);
            robot.MoveFragment();
            yield return new WaitForSeconds(0.5f);
            wc2.MoveForward(6);
            yield return new WaitForSeconds(8f);
            robot.MoveFragment();

            wc2.TurnRight(60);
            yield return new WaitForSeconds(1f);
            wc2.MoveForward(12);
            yield return new WaitForSeconds(4f);
            wc2.TurnLeft(100);
            yield return new WaitForSeconds(2f);
            wc2.MoveForward(1);
            yield return new WaitForSeconds(0.3f);
            robot.OpenLowGoal();
            yield return new WaitForSeconds(0.5f);
            robot.FireSuppressorLow();
        }
        if (robot.startingPosition == "left")
        {
            wc2.TurnRight(10);
            yield return new WaitForSeconds(0.5f);
            wc2.MoveForward(10);
            yield return new WaitForSeconds(3f);

            robot.MoveFragment();
            yield return new WaitForSeconds(3f);
            wc2.TurnRight(8);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(12);
            yield return new WaitForSeconds(3f);
            robot.MoveFragment();
            yield return new WaitForSeconds(1.5f);

            robot.MoveFragment();
            yield return new WaitForSeconds(1f);
            wc2.MoveForward(16);
            yield return new WaitForSeconds(5f);
            robot.MoveFragment();
        }
    }
    private IEnumerator Endgame1()
    {
        if (robot.startingPosition == "right")
        {
            yield return new WaitForSeconds(4f);
            wc2.MoveBack(12);
            yield return new WaitForSeconds(5f);
            robot.CloseDoor();
        }
        if (robot.startingPosition == "middle")
        {
            yield return new WaitForSeconds(4f);
            wc2.MoveBack(3);
            yield return new WaitForSeconds(2f);
            robot.CloseDoor();
        }
        if (robot.startingPosition == "left")
        {
            yield return new WaitForSeconds(4f);
            wc2.MoveBack(7);
            yield return new WaitForSeconds(3f);
            robot.CloseDoor();
        }
    }
    private IEnumerator Endgame2()
    {
        if (robot.startingPosition == "right")
        {
            yield return new WaitForSeconds(3f);
            wc2.MoveForward(1);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(1);
            yield return new WaitForSeconds(1f);

            wc2.TurnRight(80);
            yield return new WaitForSeconds(1f);
            wc2.MoveForward(4);
            yield return new WaitForSeconds(7f);

            wc2.MoveForward(1);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(1);
        }
        if (robot.startingPosition == "middle")
        {
            yield return new WaitForSeconds(3.5f);
            wc2.MoveForward(1);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(1);
            yield return new WaitForSeconds(1f);

            wc2.TurnRight(100);
            yield return new WaitForSeconds(1f);
            wc2.MoveForward(3);
            yield return new WaitForSeconds(1.5f);
            wc2.TurnRight(50);
            yield return new WaitForSeconds(5.5f);

            wc2.MoveForward(1);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(1);
        }
        if (robot.startingPosition == "left")
        {
            yield return new WaitForSeconds(4f);
            wc2.MoveForward(1);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(1);
            yield return new WaitForSeconds(1f);

            wc2.TurnLeft(90);
            yield return new WaitForSeconds(1f);
            wc2.MoveForward(4);
            yield return new WaitForSeconds(2f);
            wc2.TurnLeft(50);
            yield return new WaitForSeconds(1f);
            wc2.MoveForward(4);
            yield return new WaitForSeconds(2f);
            wc2.TurnRight(50);
            yield return new WaitForSeconds(2f);

            wc2.MoveForward(1);
            yield return new WaitForSeconds(1f);
            wc2.MoveBack(1);
        }
    }
}
