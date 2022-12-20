using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController _charController;
    [SerializeField] private float speed;
    [SerializeField] private float gravit;
    [SerializeField] private float jump_force;
    // Start is called before the first frame update

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }
    void Update()
    {

        float jump_speed = 0.0f;
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        speed = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _charController.isGrounded)
        {
            jump_speed = jump_force;

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!_charController.isGrounded)
            {
                jump_speed -= gravit * Time.deltaTime;
            }
            Vector3 distination = new Vector3(transform.forward.x, 0.0f, transform.forward.z);
            distination *= speed * Time.deltaTime;
            distination.y += jump_speed;
            _charController.Move(distination);
        }
        else
        {
            if (!_charController.isGrounded)
            {
                jump_speed -= gravit * Time.deltaTime;
            }
            Vector3 distination = new Vector3(hor, 0.0f, vert);
            distination *= speed * Time.deltaTime;
            distination.y += jump_speed;
            _charController.Move(distination);
        }
        
        
    }
}
