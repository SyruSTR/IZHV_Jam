using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    private float minimumVet = -30;
    private float maximumVet = 40;
    private float maximumHor = 45;
    private float minimumHor = -45;
    private float _rotationX = 0;
    private float _rotationY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rotationX = Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVet, maximumVet);
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        _rotationY = RestrictAngle(transform.localEulerAngles.y + delta, minimumHor, maximumHor);
        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
    }
        public static float RestrictAngle(float angle, float angleMin, float angleMax)
        {
            if (angle > 180)
                angle -= 360;
            else if (angle < -180)
                angle += 360;
            return angle;
        }
}
