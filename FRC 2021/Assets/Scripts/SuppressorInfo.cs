using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SuppressorInfo : MonoBehaviour
{
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    public SuppressorActions sup;
    private FieldInfo field;

    void Start()
    {
    }
    void Awake()
    {
        sup = GameObject.Find("Field").GetComponent<SuppressorActions>();
        field = GameObject.Find("Field").GetComponent<FieldInfo>();

        spawnPos = this.gameObject.transform.position;
        spawnRot = this.gameObject.transform.rotation;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            sup.suppressorList.Remove(this.gameObject);
            if (field.isGoalOpen)
            {
                field.score += 1;
            }
            sup.MakeSuppressor(spawnPos, spawnRot);
            Destroy(this.gameObject);
        }
    }
}