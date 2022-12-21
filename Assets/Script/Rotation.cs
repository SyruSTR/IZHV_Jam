using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    private float _rotationX = 0;
    private float _rotationY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rotationX = Input.GetAxis("Mouse X") * sensitivityVert;
        transform.rotation *= Quaternion.Euler(.0f,_rotationX,.0f);
    }

}
