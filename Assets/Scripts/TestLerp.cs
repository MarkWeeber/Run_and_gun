using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public float ratio = 1f;
    private float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(A.position, B.position);
        A.position = Vector3.Lerp(A.position, B.position, Time.deltaTime);
    }
}
