using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fragment : MonoBehaviour
{
    private bool isCharged;
    private Text inputCode;
    // Start is called before the first frame update
    void Start()
    {
        inputCode = GameObject.Find("Input Code").GetComponent<Text>();
        isCharged = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Red") && isCharged)
        {
            inputCode.text += "1";
            isCharged = false;
        }

        if (other.gameObject.CompareTag("Blue") && isCharged)
        {
            inputCode.text += "2";
            isCharged = false;
        }

        if (other.gameObject.CompareTag("Yellow") && isCharged)
        {
            inputCode.text += "3";
            isCharged = false;
        }

        if (other.gameObject.name.Contains("Activator"))
        {
            isCharged = true;
        }
    }
}
