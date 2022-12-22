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
    public bool hold = false;

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        
        // if (hold)
        // {
        //     gravity = 0;
        // }
        // else
        // {
        //     gravity = -4;
        // }
        // speed = 1.5f;
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     speed *= 2;
        // }
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        if (_charController.isGrounded || hold)
        {
            _jumpSpeed = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                _jumpSpeed = jumpForce;
            }
        }
        //add gravitation
        _jumpSpeed += (hold ? 0 : gravity) * Time.deltaTime;
        
        //create direction
        Vector3 velocity = new Vector3(hor, .0f, vert);
        velocity *= speed;
        velocity = transform.TransformVector(velocity);
        velocity.y = _jumpSpeed;

        _charController.Move(velocity * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag.Equals("slope"))
        {
            hold = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hold = false;
    }
}
