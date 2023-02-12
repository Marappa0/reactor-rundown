using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldInfo : MonoBehaviour
{
    private Text inputCode;
    private Text key;
    private Text scoreText;
    public Text keyDisplay;

    public bool simulation;
    public string driverPhase;

    private string currentKey;
    private int phase;

    public int score;
    public bool isGoalOpen;
    private SuppressorActions suppressor;
    System.Random rand = new System.Random();

    public GameObject[] cameraList = new GameObject[3];
    private int currentCameraIndex;

    public Transform stabilizer;
    public Transform reactorWall;

    public Transform[] doorList = new Transform[3];
    public Transform[] fragmentList = new Transform[3];
    public Transform[] dropList = new Transform[6];
    // Start is called before the first frame update
    void Start()
    {
        suppressor = this.GetComponent<SuppressorActions>();

        inputCode = GameObject.Find("Input Code").GetComponent<Text>();
        key = GameObject.Find("Key").GetComponent<Text>();
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();

        foreach (Transform t in doorList)
        {
            t.gameObject.SetActive(false);
        }
        
        cameraList[0].SetActive(true);
        cameraList[1].SetActive(false);
        cameraList[2].SetActive(false);
        currentCameraIndex = 0;

        suppressor.MakeSuppressor(new Vector3(-6.5f, 0.25f, 2f), Quaternion.identity);
        suppressor.MakeSuppressor(new Vector3(-6.5f, 0.25f, -2f), Quaternion.identity);

        score = 0;
        currentKey = "-";
        inputCode.text = "Input: ";

        if (driverPhase == "teleop")
        {
            StartCoroutine(Autonomous());
        }
        else if (driverPhase == "endgame1")
        {
            StartCoroutine(Endgame1());
        }
        else
        {
            StartCoroutine(Endgame2());
        }
    }

    //Coroutines
    private IEnumerator Autonomous()
    {
        scoreText.text = "Autonomous";
        isGoalOpen = true;
        yield return new WaitForSeconds(20);
        isGoalOpen = false;
        Debug.Log("End of autonomous...");
        StartCoroutine(Teleop());
    }
    private IEnumerator Teleop()
    {
        scoreText.text = "Teleop";
        currentKey = GenerateCode();
        inputCode.text = "Input: ";
        yield return new WaitForSeconds(100);
        if (score < 10)
        {
            StartCoroutine(Endgame1());
        }
        else
        {
            StartCoroutine(Endgame2());
        }
    }
    private IEnumerator Endgame1()
    {
        scoreText.text = "Endgame 1";
        isGoalOpen = false;
        yield return new WaitForSeconds(30);
        Debug.Log("Endgame 1 Finished!");
    }
    private IEnumerator Endgame2()
    {
        scoreText.text = "Endgame 2";
        isGoalOpen = false;
        yield return new WaitForSeconds(2);
        for (int i = 1; i <= 5; i ++)
        {
            phase = i;
            currentKey = GenerateShortCode();
            yield return new WaitForSeconds(10);
        }
        Debug.Log("Endgame 2 Finished!");
    }
    
    private IEnumerator ActivateGoal()
    {
        inputCode.text = "Input: ";
        currentKey = "-";
        isGoalOpen = true;
        stabilizer.position += new Vector3(0f, 1f, 0f);
        yield return new WaitForSeconds(10);
        isGoalOpen = false;
        stabilizer.position -= new Vector3(0f, 1f, 0f);
        currentKey = GenerateCode();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire3"))
        {
            cameraList[currentCameraIndex].SetActive(false);
            if (currentCameraIndex != 2)
            {
                cameraList[currentCameraIndex+1].SetActive(true);
                currentCameraIndex += 1;
            }
            else
            {
                cameraList[0].SetActive(true);
                currentCameraIndex = 0;
            }
        }
        
        
        keyDisplay.text = currentKey;
        key.text = "Key: " + currentKey;

        if (inputCode.text.Contains(currentKey) && Time.time >= 15 && driverPhase != "endgame2")
        {
            StartCoroutine(ActivateGoal());
        }
        if (driverPhase == "endgame2" && inputCode.text.Contains(currentKey))
        {
            inputCode.text = "Input: ";
        }
    }
    private string GenerateCode()
    {
        if (simulation && Time.time < 30)
        {
            return "321";
        }
        else if (simulation && Time.time >= 30)
        {
            return "213";
        }
        else
        {
            string code = "";
            List<string> digitPos = new List<string>() {"1", "2", "3"};
            for (int i = 0; i < 3; i++)
            {
                int index = rand.Next(0, digitPos.Count);
                code += digitPos[index];
                digitPos.RemoveAt(index);
            }
            inputCode.text = "Input: ";
            return code;
        }
    }
    private string GenerateShortCode()
    {
        if (phase == 1)
        {
            inputCode.text = "Input: ";
            return "321";
        }
        else if (phase == 2)
        {
            inputCode.text = "Input: ";
            return "213";
        }
        else
        {
            string code = rand.Next(1, 4).ToString();
            return code;
        }
    }
}
