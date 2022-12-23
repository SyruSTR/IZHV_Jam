using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Camera : MonoBehaviour
{
    public Transform objectToFollow;
    [SerializeField] private Vector3 deltapos;
    [SerializeField] private float smooth = 0.5f;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    [SerializeField] private float minimumVet = -30;
    [SerializeField] private float maximumVet = 40;
    private float maximumHor = 45;
    private float minimumHor = -45;
    private float _rotationX = 0;
    private float _rotationY = 0;

    // Start is called before the first frame update
    //void Start()
    //{
    //    deltapos = transform.position - objectToFollow.position;
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        _rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor;
        _rotationY += Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationY = Mathf.Clamp (_rotationY, minimumVet, maximumVet);
        transform.localEulerAngles = new Vector3(-_rotationY, _rotationX, 0);
        transform.position = transform.localRotation * deltapos + objectToFollow.position;
    }
  
}
