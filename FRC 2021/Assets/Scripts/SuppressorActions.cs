using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppressorActions : MonoBehaviour
{
    private int suppressorCount;
    public GameObject suppressorPrefab;
    public List<GameObject> suppressorList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        suppressorCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeSuppressor(Vector3 pos, Quaternion rot)
    {
        suppressorList.Add(Instantiate(suppressorPrefab, pos, rot));
        suppressorCount = suppressorList.Count;
    }
}
