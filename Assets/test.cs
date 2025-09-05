using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello, World");
            Rigidbody rb = GetComponent<Rigidbody>();
    rb.AddForce(100.0f, 200.0f, -100.0f); // 加える力のベクトルをVectorで入れる

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
