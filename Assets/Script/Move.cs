using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController _charController;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    private float _jumpSpeed = 0.0f;

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        if (_charController.isGrounded)
        {
            _jumpSpeed = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                _jumpSpeed = jumpForce;
            }
        }
        //add graviation
        _jumpSpeed += gravity * Time.deltaTime;
        
        //create direction
        
        Vector3 velocity = new Vector3(hor, .0f, vert);
        velocity *= speed;
        velocity = transform.TransformVector(velocity);
        velocity.y = _jumpSpeed;

        _charController.Move(velocity * Time.deltaTime);

    }
}
