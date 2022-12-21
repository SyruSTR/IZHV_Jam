using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Camera : MonoBehaviour
{
    public Transform objectToFollow;
    [SerializeField] private Vector3 deltapos;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    private float minimumVet = -30;
    private float maximumVet = 40;
    private float maximumHor = 45;
    private float minimumHor = -45;
    [SerializeField] private float _rotationX = 0;
    [SerializeField] private float _rotationY = 0;

    // Start is called before the first frame update
    //void Start()
    //{
    //    deltapos = transform.position - objectToFollow.position;
    //}

    // Update is called once per frame
    void Update()
    {
        _rotationX = Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVet, maximumVet);
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        _rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        transform.position = transform.localRotation * deltapos + objectToFollow.position;
    }
  
}
