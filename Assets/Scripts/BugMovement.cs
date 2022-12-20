using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BugMovement : MonoBehaviour
{
    private enum State
    {
        Start,
        Move,
        Idle,
        Scare,
        Run,
        MoveToFood,
        Eating,
    }

    private CharacterController _characterController;
    [SerializeField] private Transform bugNest;
    [SerializeField] private float nestRadius;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Transform _player;
    [SerializeField] private float scareLength = 0.5f;
    private Vector3 _targetToMove;
    private Vector3 _offset;
    private State _state;
    private Coroutine _idleCoroutine;
    private Vector3 _velocity;

    // Start is called before the first frame update
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        _state = State.Start;
        _targetToMove = Vector3.zero;
    }
    
    void Update()
    {
        Vector3 offsetToPlayer = transform.position - _player.position;
        offsetToPlayer.y = .0f;
        if (offsetToPlayer.magnitude < scareLength && _state != State.Run && _state != State.MoveToFood && _state != State.Eating)
        {
            _state = State.Scare;
        }
        switch (_state)
        {
                case State.Start:
                    Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * nestRadius;
                    _targetToMove = new Vector3(randomCircle.x, transform.position.y, randomCircle.y);
                    _offset = _targetToMove - transform.position;
                    _state = State.Move;
                    break;
                case State.MoveToFood:
                    _velocity = (_targetToMove - transform.position);
                    //Debug.Log(_velocity.magnitude);
                    if (_velocity.magnitude > .1f)
                    {
                        _velocity = _velocity.normalized * normalSpeed;
                        _characterController.Move(_velocity * Time.deltaTime);
                    }
                    else
                    {
                        _state = State.Eating;
                    }
                    break;
                case State.Eating:
                    break;
                case State.Move:
                    _offset = _targetToMove - transform.position;
                    if (_offset.magnitude > .1f)
                    {
                        _offset = _offset.normalized * normalSpeed;
                        _characterController.Move(_offset * Time.deltaTime);
                    }
                    else
                    {
                        _state = State.Idle;
                    }
                    break;
                case State.Idle:
                    if (_idleCoroutine == null)
                    {
                        _idleCoroutine = StartCoroutine(BugIdle());
                    }
                        
                    break;
                case State.Scare:
                    //bug near nest or no
                    //x^2 + y^2 < r^2
                    if (transform.position.x * transform.position.x + transform.position.y * transform.position.y <
                        nestRadius * nestRadius)
                    {
                        _velocity = Vector3.Normalize(offsetToPlayer) * runSpeed;
                    }
                    else
                    {
                        _velocity = Vector3.Normalize(-offsetToPlayer) * runSpeed;
                    }
                    
                    StartCoroutine(RunFromThePlayer());
                    break;
                case State.Run:
                    _characterController.Move( _velocity * Time.deltaTime);
                    break;
        }
    }

    public void FinishEating()
    {
        _state = State.Start;
    }

    public void GoingToEat(Vector3 target)
    {
        _targetToMove = target;
        _state = State.MoveToFood;
    }

    IEnumerator BugIdle()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        _state = State.Start;
        _idleCoroutine = null;
    }

    IEnumerator RunFromThePlayer()
    {
        _state = State.Run;
        yield return new WaitForSeconds(1.0f);
        _state = State.Start;
    }
}